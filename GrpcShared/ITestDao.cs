namespace GrpcShared;

public class TestItem1:ItemBase<int>
{
    
}

public class TestItem2:ItemBase<string>
{
    
}
//,ICanFetch<TestItem2>
public interface ITestDao:ICanFetch<TestItem1>
{
    
}

