#!/bin/bash

apt-key adv --keyserver pgp.mit.edu --recv-keys 5072E1F5
echo 'deb http://repo.mysql.com/apt/ubuntu/ trusty mysql-5.7' | tee /etc/apt/sources.list.d/mysql.list
apt-get update
debconf-set-selections <<< 'mysql-community-server mysql-community-server/root-pass password root'
debconf-set-selections <<< 'mysql-community-server mysql-community-server/re-root-pass password root'
apt-get -y install mysql-server
printf '[mysqld]\nlower_case_table_names=1\nbind-address=0.0.0.0\n' | tee -a /etc/mysql/conf.d/mysqld_safe_syslog.cnf
mysql -uroot -proot -e "GRANT ALL PRIVILEGES ON *.* TO 'root'@'%' IDENTIFIED BY 'root';"
service mysql restart
