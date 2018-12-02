### Build App

```
dotnet publish BenchmarkSQLite.sln --configuration Release
```

### Build Docker Image

```
docker build --tag benchmarksqlite .
```

### Run Container in background

```
docker run --rm --interactive --tty benchmarksqlite
```

### Remove image

```
docker image rm benchmarksqlite
```
