cd ..
virtualenv --no-site-packages --distribute gae
pypm -E gae install -r gae\requirements.txt
