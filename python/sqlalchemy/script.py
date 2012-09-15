import sqlalchemy as db
import sqlalchemy.dialects.mssql as mssql
import urllib

quoted = urllib.quote_plus('Driver={SQL Server Native Client 10.0};Server=.\\SQLEXPRESS;Database=SqlAlchemy;Trusted_Connection=yes')
db_engine = db.create_engine('mssql+pyodbc:///?odbc_connect=' + quoted, echo=True)

db_metadata = db.MetaData()
users_table = db.Table('users', db_metadata, 
	db.Column('id', mssql.UNIQUEIDENTIFIER(), primary_key=True),
	db.Column('name', db.Unicode(50))
)

# db_metadata.create_all(db_engine)

print(users_table.insert())
