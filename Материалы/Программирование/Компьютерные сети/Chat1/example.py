from flask import Flask, request, render_template, jsonify, escape, redirect
import sqlite3
from datetime import datetime


app = Flask(__name__)
app.debug = True


@app.route('/')
def index():
    return render_template('index.html')


@app.route('/api/messages/list')
def api_messages_list():
    conn = sqlite3.connect('guess.db')
    #conn.row_factory = sqlite3.Row
    c = conn.cursor()
    msgs = c.execute('''SELECT username, text, time FROM messages ORDER BY id DESC ;''').fetchall()

    msgs = [[escape(x), escape(y), z] for x,y,z in msgs]

    return jsonify(messages=msgs)


@app.route('/api/messages/add', methods=['POST'])
def api_messages_add():
    message = request.get_json()
    conn = sqlite3.connect('guess.db')
    c = conn.cursor()
    time_now = datetime.strftime(datetime.now(), "%Y.%m.%d %H:%M:%S")
    c.execute('''INSERT INTO messages(username, text, time) VALUES(?, ?, ?);''', [message['text'], message['user'], time_now])
    conn.commit()
    return 'OK'

@app.route('/delete_messages', methods=['POST'])
def clear():
    conn = sqlite3.connect('guess.db')
    if request.method == 'POST':
        c = conn.cursor()
        c.execute('Delete FROM messages')
        conn.commit()
    return redirect('/')

@app.route('/delete_checked_messages', methods=['POST'])
def clear_checked():
    conn = sqlite3.connect('guess.db')
    times = request.get_json()
    if request.method == 'POST':
        c = conn.cursor()
        for i in range(0, len(times)):
            c.execute('DELETE from messages where time=:t', {"t":times[i]})
        conn.commit()
    return 'OK'



if __name__ == '__main__':
    app.run(host='0.0.0.0')