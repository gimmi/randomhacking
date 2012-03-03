from setuptools import setup, find_packages

setup(
    name='pyprj',
    version='1.0',
    packages=find_packages('src'),
    package_dir={'': 'src'},
    entry_points={
        'console_scripts': [
            'app_server = pyprj.server:app_server'
        ]
    }
)
