from flask import Flask, request, render_template, redirect
import sqlite3

app = Flask(__name__)
app.debug = True

@app.route('/', methods=['GET', 'POST'])
def index():
    if request.method == 'GET':
        conn = sqlite3.connect('guestbook.db')
        conn.row_factory = sqlite3.Row
        c = conn.cursor()
        rows = c.execute('SELECT username, text FROM messages LIMIT 10;').fetchall()
        return render_template('index.html', rows=rows)
    else:
        # вытащить из формы
        # добавить в базу
        return redirect('/')

if __name__ == '__main__':
    app.run()