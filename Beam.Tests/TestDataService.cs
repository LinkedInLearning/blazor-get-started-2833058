using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Beam.Client.Services;
using Beam.Shared;
using System.Linq;

namespace Beam.Tests{
    public class TestDataService : IDataService
    {
        public IReadOnlyList<Frequency> Frequencies {get; private set;} = 
                new List<Frequency>() { new Frequency() { Id = 1, Name ="1"}};

        public IReadOnlyList<Ray> Rays => throw new NotImplementedException();

        public User CurrentUser { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int SelectedFrequency { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action UdpatedFrequencies;
        public event Action UpdatedRays;

        public Task AddFrequency(string Name)
        {
            var list = new List<Frequency>();
            list.AddRange(Frequencies);
            list.Add(new Frequency() { Id = Frequencies.Max(f => f.Id) + 1, Name = Name });
            Frequencies = list;
            UdpatedFrequencies?.Invoke();
            return Task.CompletedTask;
        }

        public Task CreateRay(string text)
        {
            throw new NotImplementedException();
        }

        public Task GetFrequencies()
        {
            return Task.CompletedTask;
        }

        public Task<User> GetOrCreateUser(string newName = null)
        {
            throw new NotImplementedException();
        }

        public Task GetRays(int FrequencyId)
        {
            UpdatedRays?.Invoke();
            return Task.CompletedTask;
        }

        public Task<List<Ray>> GetUserRays(string name)
        {
            throw new NotImplementedException();
        }

        public Task PrismRay(int RayId)
        {
            throw new NotImplementedException();
        }

        public Task UnPrismRay(int RayId)
        {
            throw new NotImplementedException();
        }
    }
}