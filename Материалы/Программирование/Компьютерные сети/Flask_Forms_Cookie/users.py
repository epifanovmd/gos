from flask import Flask, request, render_template, escape
import wtforms
from wtforms.validators import NumberRange, Length
from uuid import uuid4
from hashlib import sha512
import hashlib
import flask
from flask import Flask, request, render_template, redirect, \
    url_for, make_response, session
import sqlite3
 
 
 
app = Flask(__name__)
app.debug = True

app.secret_key = 'A0Zr98j/3yX R~XHH!jmN]LWX/,?RT'

def hash(salt, password):
    return sha512((salt + password).encode("utf-8")).hexdigest()

  
class Login(wtforms.Form):
    Username=wtforms.StringField("Username",[Length(1,50)])
    Password=wtforms.PasswordField("Password",[Length(1,50)])
    submit_ok =wtforms.SubmitField('Принять')
    submit_exit = wtforms.SubmitField('Выйти')
    submit_in = wtforms.SubmitField('Войти')
    submit_clear_base = wtforms.SubmitField('Очистить базу')
    submit_preview_base = wtforms.SubmitField('Показать базу')


class Registration(wtforms.Form):
    Username=wtforms.StringField("Имя",[Length(1,50)])
    Password=wtforms.PasswordField("Пароль",[Length(1,50)])
    Confirm =wtforms.PasswordField("Повтор пароля",[Length(1,50)])
    submit_ok = wtforms.SubmitField('Принять')


@app.route('/main', methods=['GET'])
def main():
    if request.method == 'GET':
        return render_template('main.html')

@app.route('/', methods=['GET', 'POST'])
@app.route('/login', methods=['GET', 'POST'])
def login():
    form = Login(request.form)
    if request.method == 'GET':
        if get_username() != '' and get_username() != None:
            return render_template("login.html", Name=get_username(), Propusk=1, form=form)
        return render_template('login.html', form=form)
    elif request.method == 'POST' and form.validate():
        conn = sqlite3.connect('site.db')
        c = conn.cursor()
        username = form.Username.data
        password = form.Password.data
        userme = c.execute('SELECT username FROM users WHERE username=:user', {'user': username}).fetchone()
        if userme != None:
            userme = c.execute('SELECT username FROM users WHERE username=:user', {'user': username}).fetchone()[0] # Вытащить из базы
        else:
            userme = ''
        salt = c.execute('SELECT salt FROM users WHERE username=:user', {"user": username}).fetchone()
        if salt != None:
            salt = c.execute('SELECT salt FROM users WHERE username=:user', {"user": username}).fetchone()[0]  # Вытащить из базы
        else:
            salt = ''
        real_hash = c.execute('SELECT password FROM users WHERE username=:user',
                                        {"user": username}).fetchone()
        if real_hash != None:
            real_hash = c.execute('SELECT password FROM users WHERE username=:user',
                                        {"user": username}).fetchone()[0]  # Вытащить из базы
        else:
            real_hash = ''
        #print(userme, salt, real_hash)
        password_hash = hash(salt, password)
        if password_hash != real_hash or username != userme:
            return render_template('login.html', err="Такой пользователь не найден, проверьте Логин или Пароль", form=form)
        session_key = session.get('username', None)
        name = c.execute('SELECT username FROM sessions WHERE session_key=:ses', {"ses": session_key}).fetchone()
        if name != None:
            name = c.execute('SELECT username FROM sessions WHERE session_key=:ses', {"ses": session_key}).fetchone()[0]
        if name != username:
            session_key = str(uuid4()).encode("utf-8")
            # Добавить в базу вместе с user_id
            zap = (session_key, username)
            c.execute("insert into sessions(session_key, username) VALUES (?, ?);", zap)
            conn.commit()
            session['username'] = session_key
        return render_template("main.html", Name="Добро пожаловать, " + get_username())
    else:
        return render_template('login.html', form=form)


@app.route('/registration', methods=['GET', 'POST'])
def registration():
    form = Registration(request.form)
    if request.method == 'GET':
        return render_template('registration.html', form=form)
    elif request.method == 'POST' and form.validate():
        username = form.Username.data
        password = form.Password.data
        confirm = form.Confirm.data
        conn = sqlite3.connect('site.db')
        c = conn.cursor()
        if password != confirm:
            return render_template('registration.html', error="Пароли не совпадают!", form=form)

        try:
            salt = sha512(str(uuid4()).encode("utf-8")).hexdigest()
            password_hash = hash(salt, password)
            zap = (username, password_hash, salt)
            c.execute("insert into users(username, password, salt) VALUES (?, ?, ?);", zap)
            conn.commit()
            return redirect('/')
        except sqlite3.Error as e:
            return render_template("registration.html", error="Ошибка: {0}".format(e.args[0]), form=form)
    else:
        return render_template("registration.html", form=form)

@app.route('/Exit', methods=['POST']) #Очистка текущего авторизированного пользователя
def exit():
    session.pop('username', None)
    conn = sqlite3.connect('site.db')
    c = conn.cursor()
    c.execute('DELETE FROM sessions')
    conn.commit()
    return flask.redirect("/")

@app.route('/preview_bases', methods=['POST', 'GET'])
def preview():
    form = Login(request.form)
    if flask.request.method == 'POST':
        row = get_rows_users()
        row1 = get_rows_sessions()
        if get_username() != '' and get_username() != None:
            return render_template("login.html", Name=get_username(), Propusk=1, rows=row, rows1=row1, Identy=1, form=form)
        return flask.render_template('login.html', rows=row, rows1=row1, Identy=1, form=form)
    else:
        if get_username() != '' and get_username() != None:
            return render_template("login.html", Name=get_username(), Propusk=1, form=form)
        return redirect('/')

@app.route('/clear_base', methods=['POST']) #Очистка базы
def clear():
    form = Login(request.form)
    conn = sqlite3.connect('site.db')
    c = conn.cursor()
    c.execute('Delete FROM users')
    c.execute('DELETE FROM sessions')
    conn.commit()
    if get_username() != '' and get_username() != None:
        return render_template("login.html", Name=get_username(), Propusk=1, form=form)
    return redirect('/')

@app.route('/GO', methods=['POST', 'GET']) #Войти через авторизацию
def go():
    form = Login(request.form)
    return flask.render_template("main.html", Name="Добро пожаловать, " + get_username(), form=form)



def get_username():# По ключу из базы вытаскиваем username
    try:
        session_key = session.get('username', None)
    except KeyError:
        return None
    conn = sqlite3.connect('site.db')
    c = conn.cursor()
    us = c.execute('SELECT username FROM sessions WHERE session_key=:kk', {"kk": session_key}).fetchone()
    if us != None:
        us = c.execute('SELECT username FROM sessions WHERE session_key=:kk', {"kk": session_key}).fetchone()[0]
    return us


def get_rows_users():
    conn = sqlite3.connect('site.db')
    conn.row_factory = sqlite3.Row
    c = conn.cursor()
    d = c.execute('SELECT username, password, salt FROM users').fetchall()
    return d

def get_rows_sessions():
    conn = sqlite3.connect('site.db')
    conn.row_factory = sqlite3.Row
    c = conn.cursor()
    d = c.execute('SELECT username, session_key FROM sessions').fetchall()
    return d



if __name__ == '__main__':
    app.run()
