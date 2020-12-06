Install tools

```
dotnet tool install --global dotnet-counters
```

On one terminal

```
dotnet run
```

On another terminal

```
dotnet counters monitor --name SpikeDotnetCounters --counters SpikeDotnetCounters
```

More info

* [REF](https://docs.microsoft.com/en-us/dotnet/core/diagnostics/event-counter-perf)
* [REF](https://docs.microsoft.com/en-us/dotnet/core/diagnostics/event-counters)
* [REF](https://github.com/grpc/grpc-dotnet/blob/master/src/Grpc.AspNetCore.Server/Internal/GrpcEventSource.cs)
