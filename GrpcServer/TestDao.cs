using System.Reactive;
using GrpcShared;

namespace GrpcServer;

public class TestDao: ITestDao
{
    async Task<List<TestItem1>> ICanFetch<TestItem1, Unit>.FetchItemsAsync(Unit @params)
    {
        return await Task.FromResult(new List<TestItem1>(){new TestItem1(){ID = 1},new TestItem1(){ID = 2}});
    }

    /*async Task<List<TestItem2>> ICanFetch<TestItem2, Unit>.FetchItemsAsync(Unit @params)
    {
        return await Task.FromResult(new List<TestItem2>(){new TestItem2(){ID = "Hello"},new TestItem2(){ID = "World"}});
    }*/
}