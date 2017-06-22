from flask import Flask, render_template, request, jsonify, escape, redirect, abort, url_for, make_response
from pony.orm import *
from pytz import timezone
from datetime import datetime
from hashlib import sha512
from uuid import uuid4
from urllib.parse import quote

app = Flask(__name__)
app.debug = True

db = Database()


class Messages(db.Entity):
    _table_ = "Messages"
    id = PrimaryKey(int, auto=True)
    name = Required(str)
    message = Required(str)
    time = Required(datetime)


class Sessions(db.Entity):
    _table_ = "Sessions"
    id = PrimaryKey(int, auto=True)
    key = Required(str, 36, unique=True, index=True)
    user_id = Required(int)


class Users(db.Entity):
    _table_ = "Users"
    id = PrimaryKey(int, auto=True)
    name = Required(str, 40)
    username = Required(str, 40, unique=True, index=True)
    password = Required(str, 128)
    salt = Required(str, 128)

# sql_debug(True)

db.bind('sqlite', 'chat.sqlite', create_db=True)
#db.bind('mysql', host="bananowars.mysql.pythonanywhere-services.com", user="bananowars", passwd="bnw380934647648", db="bananowars$Messages")
#db.bind('postgres', user='pony', password='pony', host='localhost', database='Messages')
#db.bind('oracle', 'Messages/pony@localhost')

#db.drop_table("Messages", if_exists=True, with_all_data=True)
#db.drop_table("Users", if_exists=True, with_all_data=True)
#db.drop_table("Sessions", if_exists=True, with_all_data=True)
db.generate_mapping(create_tables=True)


@db_session
def add_message(name, message, time):
    Messages(name=name, message=message, time=time)
    commit()


@db_session
def get_old_messages(ofsetMsg):
    return select(r for r in Messages).order_by(Messages.id.desc())[ofsetMsg:ofsetMsg + 10]


@db_session
def get_messages():
    return select(r for r in Messages).order_by(Messages.id.desc())[:10]


@db_session
def get_message():
    return select(r for r in Messages).order_by(Messages.id.desc())[:1]


@db_session
def add_session(session_key, user_id):
    Sessions(key=session_key, user_id=user_id)
    commit()


@db_session
def delete_session(session_key):
    delete(p for p in Sessions if p.key == session_key)
    commit()


@db_session
def add_user(name, username, password_hash, salt):
    Users(name=name, username=username, password=password_hash, salt=salt)
    commit()


@db_session
def get_user_byUsername(username):
    return select(r for r in Users if r.username == username).get()


@db_session
def get_userid():
    session_key = request.cookies.get('session_key')
    if session_key == None:
        return None
    user = select(r for r in Sessions if r.key == session_key).get()
    if user == None:
        return None
    return user.user_id


@db_session
def get_username(user_id):
    return select(r for r in Users if r.id == user_id).get().username


@db_session
def get_salt():
    return select(r for r in Users if r.id == get_userid()).get().salt


@db_session
def get_password_hash():
    return select(r for r in Users if r.id == get_userid()).get().password


@db_session
def change_password(password_hash):
    select(r for r in Users if r.id == get_userid()
           ).get().password = password_hash
    commit()


@db_session
def change_name(name):
    select(r for r in Users if r.id == get_userid()).get().name = name
    commit()


def hash(salt, password):
    return sha512((salt + password).encode('utf-8')).hexdigest()


@app.route("/", methods=['GET', 'POST'])
def index():
    if request.method == 'GET':
        user_id = get_userid()
        if user_id == None:
            return redirect(url_for('login'))
        return render_template('index.html')


@app.route("/api/messages/oldmessagelist", methods=['POST'])
def api_messages_oldmessagelist():
    ofsetMsg = request.get_json()
    msg = get_old_messages(ofsetMsg['ofsetMsg'])
    messages = [[escape(x.name), escape(x.message), datetime.strftime(
        x.time, "%d.%m.%Y %H:%M:%S")] for x in msg]
    if len(msg) == 0:
        return jsonify(error="False")
    return jsonify(messages=messages)


@app.route("/api/messages/list")
def api_messages_list():
    msg = get_messages()
    messages = [[escape(x.name), escape(x.message), datetime.strftime(
        x.time, "%d.%m.%Y %H:%M:%S")] for x in msg]
    if len(msg) == 0:
        return jsonify(error="False")
    return jsonify(messages=messages, messageId=msg[0].id)


@app.route("/api/messages/NewMessage")
def api_messages_newmessage():
    msg = get_message()
    message = [[escape(x.name), escape(x.message), datetime.strftime(
        x.time, "%d.%m.%Y %H:%M:%S")] for x in msg]
    if len(msg) == 0:
        return jsonify(error="False")
    return jsonify(message=message, messageId=msg[0].id)


@app.route('/api/messages/add', methods=['POST'])
def api_messages_add():
    message = request.get_json()
    tz = timezone('Europe/Moscow')
    time = datetime.now(tz)
    add_message(message['name'], message['message'], time)
    return "OK"


@app.route('/registration', methods=['GET', 'POST'])
def registration():
    if request.method == 'GET':
        user_id = get_userid()
        if user_id != None:
            return redirect(url_for('index'))
        return render_template('registration.html')
    reg = request.get_json()
    name = reg['name']
    username = reg['username']
    password = reg['password']
    confirm = reg['confirm']
    salt = sha512(str(uuid4()).encode('utf-8')).hexdigest()
    password_hash = hash(salt, password)
    try:
        add_user(name, username, password_hash, salt)
    except CommitException:
        return jsonify(response="Error")
    return jsonify(response="OK")


@app.route('/login', methods=['GET', 'POST'])
def login():
    if request.method == 'GET':
        user_id = get_userid()
        if user_id != None:
            return redirect(url_for('index'))
        return render_template('login.html')
    login = request.get_json()
    username = login['username']
    password = login['password']
    user = get_user_byUsername(username)
    if user is None:
        return jsonify(response="Error")
    name = user.name
    user_id = user.id
    salt = user.salt
    real_hash = user.password
    password_hash = hash(salt, password)
    if password_hash != real_hash:
        return jsonify(response="Error")
    session_key = str(uuid4())
    add_session(session_key, user_id)
    response = make_response(jsonify(response="OK"))
    response.set_cookie('session_key', session_key)
    response.set_cookie('name', quote(name))
    return response


@app.route('/logout', methods=['POST'])
def logout():
    session_key = request.cookies.get('session_key')
    delete_session(session_key)
    response = make_response("OK")
    response.set_cookie('session_key', "none")
    response.set_cookie('name', "undefined")
    return response


@app.route("/api/change/password", methods=['POST'])
def api_change_password():
    psw = request.get_json()
    oldPsw = psw['oldPsw']
    newPsw = psw['newPsw']
    salt = get_salt()
    password_hash = hash(salt, oldPsw)
    real_hash = get_password_hash()
    if password_hash != real_hash:
        return jsonify(response="Error")

    new_hash = hash(salt, newPsw)
    change_password(new_hash)
    return jsonify(response="OK")


@app.route("/api/change/name", methods=['POST'])
def api_change_name():
    name = request.get_json()['name']
    change_name(name)
    try:
        change_name(name)
    except CommitException:
        return jsonify(response="Error")
    response = make_response(jsonify(response="OK"))
    response.set_cookie('name', quote(name))
    return response

app.run()
