function setUserDir (fileName) {
	var filePath = new java.io.File(fileName).getParentFile().getCanonicalPath();
	var fileName = new java.io.File(fileName).getName();
	var userDir = java.lang.System.getProperty('user.dir');
	try
	{
		java.lang.System.setProperty('user.dir', filePath);
		load(currentFileName);
	} finally {
		java.lang.System.setProperty('user.dir', userDir);
	}
}

// in build file
JSMAKE_LOCATION = './jsmake';
load('./jsmake/main.js');

// in jsmake
load(new java.io.File(JSMAKE_PATH, 'util/tools.js').getPath());