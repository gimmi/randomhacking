cd %~dp0
python -mvenv .env
curl -o .env\distribute_setup.py http://python-distribute.org/distribute_setup.py
.env\Scripts\python .env\distribute_setup.py
.env\Scripts\easy_install pip
