namespace GrpcShared;

public abstract class ItemBase<TKey> : IEntity<TKey>
{ 
    public virtual TKey ID { get; set; }
}
public interface IEntity<TKey>
{
    TKey ID { get; set; }
}


