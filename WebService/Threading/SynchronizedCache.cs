using System;
using System.Collections.Generic;
using System.Web;
using System.Threading;


namespace WebService.Threading
{
    public class CSynchronizedCache<TKey, TValue> : IDisposable
    {
        private readonly Dictionary<TKey, TValue> _dictionary = new Dictionary<TKey, TValue>();

        private readonly Dictionary<TKey, Object> _syncDictionary = new Dictionary<TKey, Object>();

        private readonly ManualResetEvent _mre = new ManualResetEvent(true);

        private readonly Object _syncObj = new Object();

        private Boolean _disposed;


        public Boolean TryGetValue(TKey key, out TValue value)
        {
            if (_syncDictionary.TryGetValue(key, out var syncObj))
            {
                _mre.WaitOne();

                if (_disposed) throw new ObjectDisposedException(typeof(ManualResetEvent).FullName);

                lock (syncObj)
                {
                    if (_dictionary.ContainsKey(key) && _syncDictionary.ContainsKey(key))
                    {
                        value = _dictionary[key];
                        return true;
                    }
                }
            }

            value = default(TValue);
            return false;
        }

        public TValue GetValue(TKey key)
        {
            _mre.WaitOne();

            if (_disposed) throw new ObjectDisposedException(typeof(ManualResetEvent).FullName);

            lock (_syncDictionary[key])
            {
                return _dictionary[key];
            }
        }


        public void Delete(TKey key)
        {
            if (_syncDictionary.TryGetValue(key, out var syncObj))
            {
                _mre.WaitOne();

                lock (syncObj)
                {
                    if (_dictionary.ContainsKey(key) && _syncDictionary.ContainsKey(key))
                    {
                        _dictionary.Remove(key);
                        _syncDictionary.Remove(key);
                    }
                }
            }
        }

        public Boolean TrySetValue(TKey key, TValue value)
        {
            if (_syncDictionary.TryGetValue(key, out var syncObj))
            {
                _mre.WaitOne();

                lock (syncObj)
                {
                    if (_dictionary.ContainsKey(key) && _syncDictionary.ContainsKey(key))
                    {
                        _dictionary[key] = value;
                        return true;
                    }
                }
            }

            return false;
        }

        public void SetValue(TKey key, TValue value)
        {
            _mre.WaitOne();

            lock (_syncDictionary[key])
            {
                _dictionary[key] = value;
            }
        }


        public Boolean TryAdd(TKey key, TValue value)
        {
            if (!_dictionary.ContainsKey(key))
            {
                lock (_syncObj)
                {
                    _mre.Reset();

                    if (!_dictionary.ContainsKey(key))
                    {
                        foreach (Object syncObj in _syncDictionary.Values)
                            Monitor.Enter(syncObj);

                        _dictionary.Add(key, value);
                        var newSyncObj = new Object();
                        _syncDictionary.Add(key, newSyncObj);

                        foreach (Object syncObj in _syncDictionary.Values)
                            if (syncObj != newSyncObj)
                                Monitor.Exit(syncObj);

                        _mre.Set();
                        return true;
                    }

                    _mre.Set();
                }
            }

            return false;
        }

        public void Add(TKey key, TValue value)
        {
            if (!TryAdd(key, value))
                throw new Exception($"Key = {key}  and Value = {value} is already exist in dictionary");
        }


        private void InternalDispose()
        {
            lock (_syncObj)
            {
                if (_disposed) return;
                _disposed = true;

                _mre.Dispose();
                GC.SuppressFinalize(this);
            }
        }

        public void Dispose()
        {
            if (!_disposed)
                InternalDispose();
        }

        ~CSynchronizedCache()
        {
            InternalDispose();
        }

    }
}