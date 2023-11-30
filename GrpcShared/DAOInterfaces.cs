using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using System.ServiceModel;

namespace GrpcShared;

/// <summary>
/// Интерфейс для классов, реализующих доступ к данным на чтение.
/// </summary>
/// <typeparam name="TItem">Тип итема.</typeparam>
/// <typeparam name="TParams">Тип параметров для запроса.</typeparam>
[ServiceContract]
public interface ICanFetch<TItem, TParams>
{
    [OperationContract]
    Task<List<TItem>> FetchItemsAsync(TParams @params);
}

/// <summary>
/// Интерфейс для классов доступа к данным на чтение (без входящих параметров).
/// </summary>
/// <typeparam name="TItem">Тип итема.</typeparam>
[ServiceContract]
public interface ICanFetch<TItem> : ICanFetch<TItem, Unit> { }

public interface ICanUpdate<TItem>
{
    Task UpdateItemAsync(TItem item);
}

/// <summary>
/// Интерфейс для классов доступа к данным на модификацию (добавление, обновление, удаление).
/// </summary>
/// <typeparam name="TItem">Тип итема.</typeparam>
public interface ICanManipulate<TItem> : ICanUpdate<TItem>
{
    Task InsertItemAsync(TItem item);
    Task DeleteItemAsync(TItem item);
}

/// <summary>
/// Интерфейс для классов доступа к данным на добавление, обновление, удаление.
/// </summary>
/// <typeparam name="TItem">Тип итема.</typeparam>
public interface ICanArchive<TItem>
{
    Task ArchiveItemAsync(TItem item);
    Task RestoreItemAsync(TItem item);
    Task<bool> CanPermanentlyDeleteItemAsync(TItem item);
}

public interface IEditDAO<TItem, TFetchParams> : ICanFetch<TItem, TFetchParams>, ICanUpdate<TItem> { }
public interface IEditDAO<TItem> : IEditDAO<TItem, Unit>, ICanFetch<TItem> { }
public interface ISimpleCrudDAO<TItem, TFetchParams> : IEditDAO<TItem, TFetchParams>, ICanManipulate<TItem> { }
public interface ISimpleCrudDAO<TItem> : ISimpleCrudDAO<TItem, Unit>, ICanFetch<TItem> { }
public interface IArchivableCrudDAO<TItem, TFetchParams> : ISimpleCrudDAO<TItem, TFetchParams>, ICanArchive<TItem> { }
public interface IArchivableCrudDAO<TItem> : IArchivableCrudDAO<TItem, Unit>, ISimpleCrudDAO<TItem> { }