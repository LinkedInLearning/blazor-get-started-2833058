using System.Net.Http;
using System.Net.Http.Json;
using Beam.Shared;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Beam.Client.Services
{
    public class BeamApiService
    {
        HttpClient http;

        public BeamApiService(HttpClient httpClient)
        {
            http = httpClient;
        }

        public Task<List<Ray>> UserRays(string name)
        {
            return http.GetFromJsonAsync<List<Ray>>
                            ($"/api/Ray/user/{name}");
        }
        public Task<List<Frequency>> FrequencyList()
        {
            return http.GetFromJsonAsync<List<Frequency>>("api/Frequency/All");
        }

        public Task<List<Ray>> RayList(int frequencyId)
        {
            return http.GetFromJsonAsync<List<Ray>>($"api/Ray/{frequencyId}");
        }

        public async Task<List<Frequency>> AddFrequency(Frequency frequency)
        {
            var resp = await http.PostAsJsonAsync("api/Frequency/Add", frequency);
            return await resp.Content.ReadFromJsonAsync<List<Frequency>>();
        }

        public async Task<List<Ray>> AddRay(Ray ray)
        {
            var resp = await http.PostAsJsonAsync("api/Ray/Add", ray);
            return await resp.Content.ReadFromJsonAsync<List<Ray>>();
        }

        public Task<User> GetUser(string name){
            return http.GetFromJsonAsync<User>($"api/User/Get/{name}");
        }

        public async Task<List<Ray>> PrismRay(Prism prism)
        {
            var resp = await http.PostAsJsonAsync("api/Prism/Add", prism);
            return await resp.Content.ReadFromJsonAsync<List<Ray>>();
        }

        public Task<List<Ray>> UnPrismRay(int rayId, int userId)
        {
            return http.GetFromJsonAsync<List<Ray>>($"api/Prism/Remove/{userId}/{rayId}");
        }
    }
}