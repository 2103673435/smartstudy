from flask import Flask
from flask_cors import CORS
from flask_sqlalchemy import SQLAlchemy


app = Flask(__name__)
app.config["SQLALCHEMY_DATABASE_URI"] = "sqlite:///auth.db"
CORS(app)
db = SQLAlchemy(app)

if __name__ == "__main__":
    app.run(port=5001)