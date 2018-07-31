// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: contracts.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Spikegrpc.Proto {
  public static partial class EchoService
  {
    static readonly string __ServiceName = "spikegrpc.proto.EchoService";

    static readonly grpc::Marshaller<global::Spikegrpc.Proto.EchoRequest> __Marshaller_EchoRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Spikegrpc.Proto.EchoRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Spikegrpc.Proto.EchoResponse> __Marshaller_EchoResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Spikegrpc.Proto.EchoResponse.Parser.ParseFrom);

    static readonly grpc::Method<global::Spikegrpc.Proto.EchoRequest, global::Spikegrpc.Proto.EchoResponse> __Method_Echo = new grpc::Method<global::Spikegrpc.Proto.EchoRequest, global::Spikegrpc.Proto.EchoResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Echo",
        __Marshaller_EchoRequest,
        __Marshaller_EchoResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Spikegrpc.Proto.ContractsReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of EchoService</summary>
    public abstract partial class EchoServiceBase
    {
      public virtual global::System.Threading.Tasks.Task<global::Spikegrpc.Proto.EchoResponse> Echo(global::Spikegrpc.Proto.EchoRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for EchoService</summary>
    public partial class EchoServiceClient : grpc::ClientBase<EchoServiceClient>
    {
      /// <summary>Creates a new client for EchoService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public EchoServiceClient(grpc::Channel channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for EchoService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public EchoServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected EchoServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected EchoServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual global::Spikegrpc.Proto.EchoResponse Echo(global::Spikegrpc.Proto.EchoRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Echo(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Spikegrpc.Proto.EchoResponse Echo(global::Spikegrpc.Proto.EchoRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Echo, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Spikegrpc.Proto.EchoResponse> EchoAsync(global::Spikegrpc.Proto.EchoRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return EchoAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Spikegrpc.Proto.EchoResponse> EchoAsync(global::Spikegrpc.Proto.EchoRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Echo, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override EchoServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new EchoServiceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(EchoServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_Echo, serviceImpl.Echo).Build();
    }

  }
}
#endregion
