// See https://aka.ms/new-console-template for more information
using System;
using System.Linq;
using System.Reflection;
using DryIoc;
using GrpcShared;
using ServiceModel.Grpc.Client;
using Grpc.Core;

Console.WriteLine("Hello, World!");


// create a channel
var channel = new Channel("0.0.0.0",5010,ChannelCredentials.Insecure);

// create a client factory
var clientFactory = new ClientFactory();

var methodInfo = typeof(ClientFactory)
    .GetMethod(nameof(ClientFactory.CreateClient),new[] { typeof(ChannelBase) })!;
//BindingFlags.Public | BindingFlags.Instance,
   //Type.DefaultBinder, new[] { typeof(ChannelBase) }, null
object CreateClient(Type contractType, ChannelBase channel) 
    => methodInfo
        .MakeGenericMethod(contractType)
        .Invoke(clientFactory, new object[] { channel });

// request the factory to generate a proxy for ICalculator service
//var calculator = clientFactory.CreateClient<ITestDao>(channel);

IContainer container = new Container(rules=>rules.WithDynamicRegistration((type, _) => new[] 
{
    new DynamicRegistration(DelegateFactory.Of(_ => CreateClient(type, channel))) 
},DynamicRegistrationFlags.Service));
var testDao = container.Resolve<ITestDao>();
var test1 = await ((ICanFetch<TestItem1>)testDao).FetchItemsAsync(default);
//var test2 = await ((ICanFetch<TestItem2>)testDao).FetchItemsAsync(default);
Console.WriteLine("");

