import flask
import sqlite3

app = flask.Flask(__name__)
app.debug = True


@app.route('/', methods=['GET', 'POST']) #Обработка вывода строк и  нажатие на кнопку
def index():

    if flask.request.method == 'GET':
        return flask.render_template('index.html')
    else:
        conn = sqlite3.connect('Base_Urls.db')
        conn.row_factory = sqlite3.Row
        row = get_rows()
        conn = sqlite3.connect('Base_Urls.db')
        c = conn.cursor()
        kol = len(row)
        try:
            dob = (flask.request.form['Url'], int(flask.request.form['Key']) + kol)
            if flask.request.form['Url'] == '':
                return flask.render_template('index.html', err1='Пустая ссылка, Зачем?')
        except ValueError:
            if flask.request.form['Url'] == '':
                return flask.render_template('index.html', err='Ошибка ввода, написано же число типа int!', err1='Пустая ссылка, Зачем?')
            else:
                return flask.render_template('index.html', err='Ошибка ввода, написано же число типа int!')
        c.execute('BEGIN  TRANSACTION')
        cell_urls = 'http://127.0.0.1:5000/' + str(int(flask.request.form['Key']) + kol)
        zapros = (flask.request.form['Url'], int(flask.request.form['Key']) + kol, cell_urls)
        c.execute('insert into Urls(url, identify, created_url) values (?, ?, ?);', zapros)
        conn.commit()
        return flask.render_template('index.html', val=cell_urls)

@app.route('/<int:uid>', methods=['GET']) #обработка адреса с идентификатором
def redir(uid):
    conn = sqlite3.connect('Base_Urls.db')
    c = conn.cursor()
    ur = c.execute('SELECT url FROM Urls WHERE identify=:id', {"id":uid}).fetchone()[0]
    if ur == '':
        return flask.abort(404)
    return flask.redirect('http://' + ur)


@app.route('/clear_base', methods=['POST']) #Очистка базы
def clear():
    conn = sqlite3.connect('Base_Urls.db')
    c = conn.cursor()
    c.execute('Delete FROM Urls')
    conn.commit()
    return flask.redirect('/')

@app.route('/preview_base', methods=['POST', 'GET'])
def preview():
    if flask.request.method == 'POST':
        row = get_rows()
        return flask.render_template('index.html', rows=row, Identy=1)
    else:
        return flask.redirect('/')

def get_rows():
    conn = sqlite3.connect('Base_Urls.db')
    conn.row_factory = sqlite3.Row
    c = conn.cursor()
    d = c.execute('SELECT id, url, identify, created_url FROM Urls').fetchall()
    return d


if __name__ == '__main__':
    app.run()