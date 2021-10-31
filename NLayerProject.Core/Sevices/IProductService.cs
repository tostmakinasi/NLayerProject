using NLayerProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Core.Sevices
{
    public interface IProductService:IService<Product>
    {
        //Product ile ilgili veri tabanıyla ilgisi olmayan methodların tanımlanacağı yer
        //bool ControlInnerBarcode(Product product);
        Task<Product> GetWithCategoryByIdAsync(int productId);
    }
}
