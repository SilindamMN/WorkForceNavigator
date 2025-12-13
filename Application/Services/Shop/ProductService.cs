using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Application.Helpers;
using Application.Interfaces.GenericInterfaces;
using Application.Interfaces.Shop;
using AutoMapper;
using Domain.Dtos.General;
using Domain.Dtos.Shop;
using Domain.Enties.Shop;

namespace Application.Services.Shop
{
    public class ProductService : IProductService
    {
        private readonly IGenericService<Product, ProductDto> _genericService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;

        public ProductService(
            IGenericService<Product, ProductDto> genericService,
            IHttpClientFactory httpClientFactory,
            IMapper mapper)
        {
            _genericService = genericService;
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
        }

        private HttpClient CreateClient()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://fakestoreapi.com/");
            return client;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
            => await _genericService.GetAllAsync();

        public async Task<ProductDto?> GetProductByIdAsync(int id)
            => await _genericService.GetByIdAsync(id);
        public async Task<GeneralServiceResponseDto> SyncFromExternalApiAsync()
        {
            var client = CreateClient();
            var externalProducts = await client.GetFromJsonAsync<List<FakeStoreProductDto>>("products");

            if (externalProducts == null || !externalProducts.Any())
                return ResponseHelper.CreateResponse(false, 400, "No products found in external API");

            // Get all existing products from DB
            var allProducts = (await _genericService.GetAllAsync())
                .ToDictionary(p => p.ExternalProductId, p => p);

            foreach (var ext in externalProducts)
            {
                // Map external id -> ExternalProductId
                var extId = ext.id;

                if (allProducts.TryGetValue(extId, out var existing))
                {
                    // Update existing product if changed
                    if (existing.Name != ext.Title ||
                        existing.Price != ext.Price ||
                        existing.Category != ext.Category ||
                        existing.ImageUrl != ext.Image)
                    {
                        existing.Name = ext.Title;
                        existing.Price = ext.Price;
                        existing.Category = ext.Category;
                        existing.ImageUrl = ext.Image;

                        await _genericService.UpdateAsync(existing.Id, existing);
                    }
                }
                else
                {
                    // Insert new product
                    var dto = new ProductDto
                    {
                        Name = ext.Title,
                        Price = ext.Price,
                        Category = ext.Category,
                        ImageUrl = ext.Image,
                        ExternalProductId = extId
                    };

                    await _genericService.CreateAsync(dto);
                }
            }

            return ResponseHelper.CreateResponse(true, 200, "Products synced successfully");
        }

    }
}