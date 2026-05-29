using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dtos.General;
using Domain.Dtos.GeneralAdmin;
using Domain.Dtos.Shop;

namespace Application.Interfaces.Shop
{
    public interface IProductService
    {
        Task<IEnumerable<FakeStoreProductDto>> GetAllExternalProductsAsync();
        Task<FakeStoreProductDto?> GetExternalProductByIdAsync(int id);

        Task<GeneralServiceResponseDto> CreateExternalProductAsync(FakeStoreProductDto dto);
        Task<GeneralServiceResponseDto> UpdateExternalProductAsync(int externalProductId, FakeStoreProductDto dto);
        Task<GeneralServiceResponseDto> DeleteExternalProductAsync(int externalProductId);
    }
}
