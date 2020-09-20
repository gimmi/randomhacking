### Run in docker

```
docker build -t transform-tool:latest .

docker run --rm -it `
    -v "$(Get-Location):/data" `
    transform-tool:latest `
    -i /data/in `
    -o /data/out
```