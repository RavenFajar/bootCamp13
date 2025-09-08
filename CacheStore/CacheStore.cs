namespace Cache.Main;

//Regular Dictionary
//Hapus data dengan index paling kecil yang ada di Dictionary untuk dihapus
//Data paling baru berada di index terakhir

//Pindahin per element ke temp dictionary
//Add element di temp dictionary
//Clear dictionary awal
//Pindah dari temp per element ke dictionary awal

public class CacheStore
{
    private const int _capacity = 4;
    private Dictionary<string, IContainer> _cacheStore = new Dictionary<string, IContainer>(_capacity);
    private Dictionary<string, IContainer> _temp = new Dictionary<string, IContainer>(_capacity);
    public void Add<T>(string key, T value)
    {
        if (_cacheStore.ContainsKey(key))
        {
            _cacheStore[key] = new Container<T>(value);
            return;
        }

        if (_cacheStore.Count < _capacity)
        {
            _cacheStore.Add(key, new Container<T>(value));
        }
        else
        {
            int countCache = _cacheStore.Count;
            for (int i = 0; i < countCache; i++)
            {
                if (i == 0)
                {
                    string removedKey = _cacheStore.ElementAt(i).Key;
                    _cacheStore.Remove(removedKey);
                    continue;
                }
                string keyResult = _cacheStore.ElementAt(0).Key;
                var valueResult = _cacheStore.GetValueOrDefault(keyResult);
                if (valueResult is null)
                {
                    throw new ArgumentNullException();
                }
                _cacheStore.Remove(keyResult);
                _temp.Add(keyResult, valueResult);
                if (i == countCache - 1)
                {
                    _temp.Add(key, new Container<T>(value));
                }
            }
            _cacheStore.Clear();
            int tempCount = _temp.Count;
            for (int i = 0; i < tempCount; i++)
            {
                string keyResult = _temp.ElementAt(0).Key;
                var valueResult = _temp.GetValueOrDefault(keyResult);
                if (valueResult is null)
                {
                    throw new ArgumentNullException();
                }
                _temp.Remove(keyResult);
                _cacheStore.Add(keyResult, valueResult);
            }
            _temp.Clear();
        }
    }



    public void Remove(string key)
    {
        _cacheStore.Remove(key);
        if (_cacheStore.Count > 0)
        {
            Refresh();
        }
    }
    public T GetValue<T>(string key)
    {
        if (_cacheStore.TryGetValue(key, out var container))
        {
            if (container is Container<T> typedContainer)
                return typedContainer.Data;
            throw new InvalidCastException($"Data stored with key '{key}' is not of type {typeof(T)}");
        }
        else
        {
            throw new KeyNotFoundException($"Key '{key}' not found.");
        }
    }

    private void Refresh()
    {
        int countCache = _cacheStore.Count;

        for (int i = 0; i < countCache; i++)
        {
            string keyResult = _cacheStore.ElementAt(0).Key;
            var valueResult = _cacheStore.GetValueOrDefault(keyResult);
            if (valueResult is null)
            {
                throw new ArgumentNullException();
            }
            _cacheStore.Remove(keyResult);
            _temp.Add(keyResult, valueResult);
        }
        _cacheStore.Clear();

        int tempCount = _temp.Count;
        for (int i = 0; i < tempCount; i++)
        {
            string keyResult = _temp.ElementAt(0).Key;
            var valueResult = _temp.GetValueOrDefault(keyResult);
            if (valueResult is null)
            {
                throw new ArgumentNullException();
            }
            _temp.Remove(keyResult);
            _cacheStore.Add(keyResult, valueResult);
        }
        _temp.Clear();
    }
}

internal class Container<T> : IContainer
{
    public T Data { get; set; }
    public Container(T value)
    {
        Data = value;
    }
}
internal interface IContainer { }