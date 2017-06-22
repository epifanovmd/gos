from flask import Flask, render_template, request, jsonify, escape, redirect, abort
from pony.orm import *
from datetime import datetime

db = Database()

class Urls(db.Entity):
    _table_ = "Urls"
    id = PrimaryKey(int, auto=True)
    url = Required(str)
	
#sql_debug(True)

db.bind('sqlite', 'Urls.sqlite', create_db=True)

#db.drop_table("Urls", if_exists=True, with_all_data=True)
db.generate_mapping(create_tables=True)

@db_session
def add_url(url):
    Urls(url=url)
    commit()

@db_session
def get_url_byId(urlId):
	return select(r for r in Urls if r.id == urlId).get()

@db_session
def get_shorturlId():
	return select(r for r in Urls).order_by(Urls.id.desc())[:1]


app = Flask(__name__)
#app.debug = True

@app.route("/", methods=['GET', 'POST'])
def index():
		if request.method == 'GET':
			return render_template('index.html')

@app.route('/api/urls/add', methods=['POST'])
def api_url_add():
    url = request.get_json()
    add_url(url["url"])
    return "OK"			
			


@app.route("/api/urls/shorturl")
def api_url_shorturl():
		shorturl = get_shorturlId()
		return jsonify(shorturl=shorturl[0].id)
		
@app.route("/<int:id>")
def api_url_redirect(id):
		shorturl = get_url_byId(id)
		if shorturl is None:
			abort(404)
		return redirect(shorturl.url)

app.run()