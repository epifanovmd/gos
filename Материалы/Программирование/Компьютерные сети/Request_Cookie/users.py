
from uuid import uuid4
from hashlib import sha512
import hashlib
import flask
from flask import Flask, request, render_template, redirect, \
    url_for, make_response
import sqlite3

app = Flask(__name__)
app.debug = True


def hash(salt, password):
    return sha512((salt + password).encode("utf-8")).hexdigest()


@app.route('/main', methods=['GET'])
def main():
    if request.method == 'GET':
        return render_template('main.html')


@app.route('/registration', methods=['GET', 'POST'])
def registration():
    if request.method == 'GET':
        return render_template('registration.html')
    username = request.form['username']
    password = request.form['password']
    confirm = request.form['confirm']
    conn = sqlite3.connect('site.db')
    c = conn.cursor()
    if password != confirm:
        return render_template('registration.html', error="Пароли не совпадают!")
    salt = sha512(str(uuid4()).encode("utf-8")).hexdigest()
    password_hash = hash(salt, password)
    try:
        zap = (username, password_hash, salt)
        #print(zap)
        c.execute("insert into users(username, password, salt) VALUES (?, ?, ?);", zap)
        conn.commit()
        return redirect('/')
    except sqlite3.Error as e:
        return render_template("registration.html", error="Ошибка: {0}".format(e.args[0]))

@app.route('/', methods=['GET', 'POST'])
@app.route('/login', methods=['GET', 'POST'])
def login():
    if request.method == 'GET':
        if get_username(request) != '' and get_username(request) != None:
            return render_template("login.html", Name=get_username(request), Propusk=1)
        return render_template('login.html')
    conn = sqlite3.connect('site.db')
    c = conn.cursor()

    username = request.form['username']
    password = request.form['password']
    userme = c.execute('SELECT username FROM users WHERE username=:user', {'user': username}).fetchone()
    if userme is not None:
        userme = c.execute('SELECT username FROM users WHERE username=:user', {'user': username}).fetchone()[0]
    salt = c.execute('SELECT salt FROM users WHERE username=:user', {"user": username}).fetchone()
    if salt is not None:
        salt = c.execute('SELECT salt FROM users WHERE username=:user', {"user": username}).fetchone()[0]
    real_hash = c.execute('SELECT password FROM users WHERE username=:user',
                                    {"user": username}).fetchone()
    if real_hash is not None:
        real_hash = c.execute('SELECT password FROM users WHERE username=:user',
                                    {"user": username}).fetchone()[0]
    if userme is None or real_hash is None or salt is None:
        return render_template('login.html', err="Такой пользователь не найден, проверьте Логин или Пароль")
    #print(userme, salt, real_hash)
    password_hash = hash(salt, password)
    session_key = c.execute("SELECT session_key FROM sessions WHERE username=:user;",{"user": userme}).fetchone()

    if password_hash != real_hash or username != userme:
        return render_template('login.html', err="Такой пользователь не найден, проверьте Логин или Пароль")
    if session_key == '' or session_key is None:
        session_key = str(uuid4()).encode("utf-8")
    #print(session_key)
        # Добавить в базу вместе с user_id
        zap = (session_key, username)
        c.execute("insert into sessions(session_key, username) VALUES (?, ?);", zap)
        conn.commit()
    response = make_response(render_template("main.html", Name="Добро пожаловать, " + username))
    response.set_cookie('session_key', session_key)

    return response

@app.route('/preview_bases', methods=['POST', 'GET'])
def preview():
    if flask.request.method == 'POST':
        row = get_rows_users()
        row1 = get_rows_sessions()
        if get_username(request) != '' and get_username(request) != None:
            return render_template("login.html", Name=get_username(request), Propusk=1, rows=row, rows1=row1, Identy=1)
        return flask.render_template('login.html', rows=row, rows1=row1, Identy=1)
    else:
        if get_username(request) != '' and get_username(request) != None:
            return render_template("login.html", Name=get_username(request), Propusk=1)
        return redirect('/')

@app.route('/clear_base', methods=['POST']) #Очистка базы
def clear():
    conn = sqlite3.connect('site.db')
    c = conn.cursor()
    c.execute('Delete FROM users')
    c.execute('DELETE FROM sessions')
    conn.commit()
    if get_username(request) != '' and get_username(request) != None:
        return render_template("login.html", Name=get_username(request), Propusk=1)
    return redirect('/')

@app.route('/GO', methods=['POST', 'GET']) #Войти через авторизацию
def go():
    if flask.request.method == 'GET':
        if get_username(request) is None:
            return redirect('/')
        else:
            return flask.render_template("main.html", Name="Добро пожаловать, " + get_username(request))
    else:
        if get_username(request) is None:
            return redirect('/')
        else:
            return flask.render_template("main.html", Name="Добро пожаловать, " + get_username(request))

@app.route('/Exit', methods=['POST']) #Очистка текущего авторизированного пользователя
def exit():
    conn = sqlite3.connect('site.db')
    c = conn.cursor()
    c.execute('DELETE FROM sessions')
    conn.commit()
    return flask.redirect("/")


def get_username(request):# По ключу из базы вытаскиваем username
    try:
        session_key = str(request.cookies['session_key']).encode("utf-8")
    except KeyError:
        exit()
        return None
    conn = sqlite3.connect('site.db')
    c = conn.cursor()
    #print(session_key)
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
