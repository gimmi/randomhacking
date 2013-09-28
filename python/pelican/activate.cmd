PUSHD %~dp0
IF EXIST .env GOTO ACTIVATE
python -mvenv .env
curl -o .env\distribute_setup.py http://python-distribute.org/distribute_setup.py
.env\Scripts\python .env\distribute_setup.py
.env\Scripts\easy_install pip
.env\Scripts\pip install -r requirements.txt
:ACTIVATE
CALL .env\Scripts\activate.bat
POPD