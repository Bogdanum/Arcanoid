public interface IDataStorage<T>
{
    void Save(T data);
    T Load(IStoredData defaultData);
}

