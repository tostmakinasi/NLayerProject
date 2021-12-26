using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.Web.ApiServices
{
    interface IApiService<TDto> where TDto : class
    {
        ApiControllerType apiControllerType { get; set; }

        Task<IEnumerable<TDto>> GetAllAsync();

        Task<TDto> AddAsync(TDto tDto);

        Task<TDto> GetByIdAsync(int id);

        Task<bool> Update(TDto tDto);

        Task<bool> Remove(int id);
    }

    public enum ApiControllerType
    {
        categories,

        products
    }
}
