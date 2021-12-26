using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Web.ApiServices
{
    public class ApiService<T> : IApiService<T> where T:class
    {
        private readonly HttpClient _httpClient;

        public ApiControllerType apiControllerType { get; set; }

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("apiService");
        }

        public async Task<T> AddAsync(T tDto)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(tDto), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{apiControllerType}", stringContent);

            if (response.IsSuccessStatusCode)
            {
                tDto = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                //loglama yap

                tDto = null;
            }

            return tDto;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            IEnumerable<T> tDtos;
            var response = await _httpClient.GetAsync("categories");

            if (response.IsSuccessStatusCode)
            {
                tDtos = JsonConvert.DeserializeObject<IEnumerable<T>>(await response.Content.ReadAsStringAsync());
            }
            else
                tDtos = null;

            return tDtos;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{apiControllerType}/{id}");

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            else
                return null;
        }

        public async Task<bool> Remove(int id)
        {
            var response = await _httpClient.DeleteAsync($"{apiControllerType}/{id}");

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Update(T tDto)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(tDto), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{apiControllerType}", stringContent);

            return response.IsSuccessStatusCode;
        }
    }

   
}
