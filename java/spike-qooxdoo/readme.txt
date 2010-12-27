Creazione applicazione qooxdoo
	cd C:\Users\Gimmi\Sources\randomhacking\java\spike-qooxdoo\src\main\webapp
	copia qooxdoo-1.3-sdk in webapp
	qooxdoo-1.3-sdk\tool\bin\create-application.py --name=spikeqooxdoo --out=.
	muovi il contenuto di spikeqooxdoo in webapp
	Elimina
		readme.txt
		generate.py
	Edit config.json
		aggiusta QOOXDOO_PATH (http://stackoverflow.com/questions/3804609/developing-with-qooxdoo-and-multiple-developers)
	qooxdoo-1.3-sdk\tool\bin\generator.py source-all