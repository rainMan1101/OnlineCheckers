using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;

namespace WebService.Threading
{
    public class CSynchronizedCacheOld<TKey, TValue> 
    {
        private Dictionary<TKey, TValue> _dictionary = new Dictionary<TKey, TValue>();

        private Dictionary<TKey, Object> _syncDictionary = new Dictionary<TKey, Object>();

        private Object _syncUpdateObj = new Object();

        private Object _syncReadObj = new Object();

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (_syncDictionary.TryGetValue(key, out var syncItemObj))
            {
                lock (_syncReadObj)
                {
                    lock (syncItemObj)
                    {
                        return _dictionary.TryGetValue(key, out value);
                    }
                }
            }

            value = default(TValue);
            return false;
        }


        public TValue GetValue(TKey key)
        {
            lock (_syncReadObj)
            {
                lock (_syncDictionary[key])
                {
                    return _dictionary[key];
                }
            }
        }


        public void SetValue(TKey key, TValue value)
        {
            if (_dictionary.ContainsKey(key))
            {
                lock (_syncUpdateObj)
                {
                    if (_syncDictionary.ContainsKey(key))
                    {
                        lock (_syncDictionary[key])
                        {
                            _dictionary[key] = value;
                        }
                    }
                }
            }
        }


        public bool TryAdd(TKey key, TValue value)
        {
            if (!_dictionary.ContainsKey(key))
            {
                lock (_syncReadObj)
                {
                    lock (_syncUpdateObj)
                    {
                        if (!_dictionary.ContainsKey(key))
                        {
                            _dictionary.Add(key, value);
                            _syncDictionary.Add(key, new Object());
                            return true;
                        }
                    }
                }
            }
            return false;
        }


        public void Add(TKey key, TValue value)
        {
            lock (_syncReadObj)
            {
                lock (_syncUpdateObj)
                {
                    _dictionary.Add(key, value);
                    _syncDictionary.Add(key, new Object());
                }
            }
        }


        public void Delete(TKey key)
        {
            if (_dictionary.ContainsKey(key))
            {
                //lock (_syncReadObj)
                //{
                    lock (_syncUpdateObj)
                    {
                        if (_dictionary.ContainsKey(key))
                        {
                            _dictionary.Remove(key);
                            _syncDictionary.Remove(key);
                        }
                    }
                //}
            }
        }


    }


}