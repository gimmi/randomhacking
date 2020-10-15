### Run in docker

```
docker build --pull -t node-udp-gelf:latest .

docker run --rm -it -p 12201:12201/udp node-udp-gelf:latest
```



docker run --rm -it --log-driver gelf –-log-opt gelf-address=udp://127.0.0.1:12201 alpine echo 'Some thing'
