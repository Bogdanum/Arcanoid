public interface IDataStorage<T>
{
    bool SaveExists();
    void Save(T data);
    T Load(IStoredData defaultData);
    void ResetData();
}

