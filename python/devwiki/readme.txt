Initialization of DEV environment
	virtualenv --distribute .
	Scripts\pip install -r requirements.txt

Freezing requirements
	Scripts\pip freeze > requirements.txt