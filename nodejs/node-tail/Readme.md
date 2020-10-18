### Build Docker image

```
docker build --no-cache -t node-tail:latest .
```

### Run Docker image

```
docker run --rm -it -e DEBUG=tail:* -p 3000:3000 -p 24225:24225 node-tail:latest
```

### Send data from fluentd

```
docker run --rm -it `
  -p 24224:24224 -p 24224:24224/udp `
  fluent/fluent-bit `
  /fluent-bit/bin/fluent-bit -i forward -o stdout -o forward://host.docker.internal:24225

docker run --rm -it `
  --log-driver fluentd `
  --log-opt tag=src-tag `
  --log-opt fluentd-address=127.0.0.1:24224 `
  --log-opt fluentd-sub-second-precision=true `
  alpine `
  echo 'Some thing'
```
