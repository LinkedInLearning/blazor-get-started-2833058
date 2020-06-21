using System;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using System.Collections.Generic;

namespace Beam.Tests
{
    public class TestLocalStorage : ILocalStorageService
    {
        private Dictionary<string, object> dictionary = new Dictionary<string, object>();
        public event EventHandler<ChangingEventArgs> Changing;
        public event EventHandler<ChangedEventArgs> Changed;

        public Task ClearAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ContainKeyAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetItemAsync<T>(string key)
        {
            if (dictionary.ContainsKey(key))
                return Task.FromResult((T)dictionary[key]);
            else
                return Task.FromResult(default(T));
        }

        public Task<string> KeyAsync(int index)
        {
            throw new NotImplementedException();
        }

        public Task<int> LengthAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveItemAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task SetItemAsync<T>(string key, T data)
        {
            Changing?.Invoke(this, new ChangingEventArgs() { Key = key, NewValue = data });
            dictionary[key] = data;
            Changed?.Invoke(this, new ChangedEventArgs() { Key = key, NewValue = data});
            return Task.CompletedTask;
        }
    }
}