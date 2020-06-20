using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Beam.Shared;

namespace Beam.Client.Services
{
    public interface IDataService
    {
        IReadOnlyList<Frequency> Frequencies { get; }
        IReadOnlyList<Ray> Rays { get; }
        User CurrentUser { get; set; }
        int SelectedFrequency { get; set; }

        event Action UdpatedFrequencies;
        event Action UpdatedRays;

        Task GetFrequencies();

        Task GetRays(int FrequencyId);

        Task<List<Ray>> GetUserRays(string name);

        Task AddFrequency(string Name);

        Task CreateRay(string text);

        Task PrismRay(int RayId);

        Task UnPrismRay(int RayId);

        Task<User> GetOrCreateUser(string newName = null);
    }
}