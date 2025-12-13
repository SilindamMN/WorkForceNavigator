using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dtos.General;
using Domain.Dtos.Shop;

namespace Application.Interfaces.Shop
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(int id);

        Task<GeneralServiceResponseDto> SyncFromExternalApiAsync();
    }
}
