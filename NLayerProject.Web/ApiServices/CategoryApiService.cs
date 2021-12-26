using Newtonsoft.Json;
using NLayerProject.Web.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Web.ApiServices
{
    public class CategoryApiService : ApiService<CategoryDto>
    {
        private HttpClient _httpClient;

        public CategoryApiService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            apiControllerType = ApiControllerType.categories;

            _httpClient = httpClientFactory.CreateClient("apiService");
        }

        public async Task<CategoryWithProductsDto> GetWithProductsByIdAsync(int categoryId)
        {

            CategoryWithProductsDto categoryWithProductsDtos;

            var response = await _httpClient.GetAsync($"categories/{categoryId}/products");

            if (response.IsSuccessStatusCode)
            {
                categoryWithProductsDtos = JsonConvert.DeserializeObject<CategoryWithProductsDto>(await response.Content.ReadAsStringAsync());
            }
            else
                categoryWithProductsDtos = null;

            return categoryWithProductsDtos;
        }

    }
}
