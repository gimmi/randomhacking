### Run in docker

```
docker build --pull -t node-udp-gelf:latest .

docker run --rm -it -p 12201:12201/udp node-udp-gelf:latest
```

### Send test messages

```
nc.exe -u 127.0.0.1 12201
{ "short_message": "xoxo", "timestamp": 1602850875.683, "_container_name": "agitated_goldberg" }
```

