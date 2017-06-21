from flask import Flask, request, render_template, redirect, escape, jsonify
import sqlite3
#from werkzeug import secure_filename
import os

app = Flask(__name__)
app.debug = True

@app.route('/', methods=['GET', 'POST'])
def index():
    if request.method == 'GET':
        return render_template('index.html') #rows=get_rows_images())
    return redirect('/')


@app.route('/api/images/add', methods=['POST'])
def api_images_add():
    conn = sqlite3.connect('Imgs.db')
    c = conn.cursor()
    file = request.files['img']
    file.save("static/public/imgs/" + file.filename)
    c.execute('INSERT INTO imgs(image_name, time) VALUES (?, datetime("now"));', [file.filename])
    conn.commit()
    return 'OK'

@app.route('/api/images/list', methods=['GET'])
def api_images_list():
    conn = sqlite3.connect('Imgs.db')
    c = conn.cursor()
    im = c.execute('''SELECT image_name, strftime('%d.%m.%Y %H:%M:%S', time) FROM imgs;''').fetchall()
    im = [[escape(x), y] for x, y in im]
    return jsonify(imgs=im)

@app.route('/delete_image', methods=['POST'])
def clear():
    conn = sqlite3.connect('Imgs.db')
    imgs = request.get_json()
    path = os.path.join(os.path.abspath(os.path.dirname(__file__)), "static/public/imgs/" + imgs['image'])
    os.remove(path)
    c = conn.cursor()
    c.execute('DELETE from imgs where image_name=:t', {"t":imgs['image']})
    conn.commit()
    return 'OK'

@app.route('/delete_all_imgs', methods=['POST'])
def clear_all():
    conn = sqlite3.connect('Imgs.db')
    c = conn.cursor()
    row = c.execute('SELECT image_name FROM imgs').fetchall()
    for i in range(0, len(row)):
        path = os.path.join(os.path.abspath(os.path.dirname(__file__)), "static/public/imgs/" + row[i][0])
        os.remove(path)
    c.execute('DELETE from imgs');
    conn.commit()
    return 'OK'



if __name__ == '__main__':
    app.run()