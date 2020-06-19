using Beam.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json; 
using System.Threading.Tasks;

namespace Beam.Client.Services
{
    public class DataService
    {
        public IReadOnlyList<Frequency> Frequencies { get; private set; }
        public IReadOnlyList<Ray> Rays { get; private set; } = new List<Ray>();
        public User CurrentUser { get; set; }
        HttpClient http;
        
        private int? selectedFrequency;
        public int SelectedFrequency
        {
            get
            {
                if (!selectedFrequency.HasValue && Frequencies.Count > 0)
                {
                    selectedFrequency = Frequencies.First().Id;
                }
                return selectedFrequency ?? 0;
            }
            set
            {
                selectedFrequency = value;
                GetRays(value).ConfigureAwait(false);
            }
        }

        public DataService(HttpClient httpClient)
        {
            http = httpClient;
            if (CurrentUser == null) CurrentUser = new User() { Name = "Anon" + new Random().Next(0, 10) };
        }

        public event Action UdpatedFrequencies;
        public event Action UpdatedRays;

        public async Task GetFrequencies()
        {
            Frequencies = await FrequencyList(); 
            UdpatedFrequencies?.Invoke();
        }

        public async Task GetRays(int FrequencyId)
        {
            Rays = new List<Ray>();
            Rays = await RayList(FrequencyId); 
            UpdatedRays?.Invoke();
        }

        public async Task<List<Ray>> GetMyRays()
        {
            return await http.GetFromJsonAsync<List<Ray>>
                ($"/api/Ray/user/{CurrentUser.Name}");
        }
        public async Task AddFrequency(string Name)
        {
            Frequencies = await AddFrequency(new Frequency() { Name = Name });  
            UdpatedFrequencies?.Invoke();
        }

        public async Task CreateRay(string text)
        {
            var ray = new Ray()
            {
                FrequencyId = selectedFrequency.Value,
                Text = text,
                UserId = CurrentUser.Id
            };

            if (CurrentUser.Id == 0)
            {
                await GetOrCreateUser();
                ray.UserId = CurrentUser.Id;
            }

            Rays = await AddRay(ray); 
            UpdatedRays?.Invoke();
        }

        public async Task PrismRay(int RayId)
        {
            if (CurrentUser.Id == 0) await GetOrCreateUser();
            Rays = await PrismRay(new Prism() { RayId = RayId, UserId = CurrentUser.Id }); 
            UpdatedRays?.Invoke();
        }

        public async Task UnPrismRay(int RayId)
        {
            if (CurrentUser.Id == 0) await GetOrCreateUser();
            Rays = await UnPrismRay(RayId, CurrentUser.Id); 
            UpdatedRays?.Invoke();
        }

        internal Task<List<Frequency>> FrequencyList()
        {
            return http.GetFromJsonAsync<List<Frequency>>("api/Frequency/All");
        }

        internal Task<List<Ray>> RayList(int frequencyId)
        {
            return http.GetFromJsonAsync<List<Ray>>($"api/Ray/{frequencyId}");
        }

        internal async Task<List<Frequency>> AddFrequency(Frequency frequency)
        {
            var resp = await http.PostAsJsonAsync("api/Frequency/Add", frequency);
            return await resp.Content.ReadFromJsonAsync<List<Frequency>>();
        }

        internal async Task<List<Ray>> AddRay(Ray ray)
        {
            var resp = await http.PostAsJsonAsync("api/Ray/Add", ray);
            return await resp.Content.ReadFromJsonAsync<List<Ray>>();
        }

        internal async Task<User> GetOrCreateUser(string newName = null)
        {
            var name = newName ?? CurrentUser.Name;
            CurrentUser = await http.GetFromJsonAsync<User>($"api/User/Get/{name}");
            return CurrentUser;
        }

        internal async Task<List<Ray>> PrismRay(Prism prism)
        {
            var resp = await http.PostAsJsonAsync("api/Prism/Add", prism);
            return await resp.Content.ReadFromJsonAsync<List<Ray>>();           
        }

        internal Task<List<Ray>> UnPrismRay(int rayId, int userId)
        {
            return http.GetFromJsonAsync<List<Ray>>($"api/Prism/Remove/{userId}/{rayId}");
        }
    }
}
