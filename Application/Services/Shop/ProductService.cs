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
        private readonly HttpClient _client;

        public ProductService()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://fakestoreapi.com/")
            };
        }

        public async Task<GeneralServiceResponseDto> CreateExternalProductAsync(FakeStoreProductDto dto)
        {
            var response = await _client.PostAsJsonAsync("products", dto);
            return response.IsSuccessStatusCode ?
                ResponseHelper.CreateResponse(true, 200, "Created Successfully") :
                ResponseHelper.CreateResponse(false, 400, "Failed to create external product");
        }

        public async Task<GeneralServiceResponseDto> UpdateExternalProductAsync(int externalProductId, FakeStoreProductDto dto)
        {
            var response = await _client.PutAsJsonAsync($"products/{externalProductId}", dto);
            return response.IsSuccessStatusCode
                ? ResponseHelper.CreateResponse(true, 200, "Product updated")
                : ResponseHelper.CreateResponse(false, 400, "Failed to update product");
        }

        public async Task<IEnumerable<FakeStoreProductDto>> GetAllExternalProductsAsync()
        {
            return await _client.GetFromJsonAsync<List<FakeStoreProductDto>>("products")
                   ?? new List<FakeStoreProductDto>();
        }

        public async Task<FakeStoreProductDto?> GetExternalProductByIdAsync(int id)
        {
            try
            {
                return await _client.GetFromJsonAsync<FakeStoreProductDto>($"products/{id}");
            }
            catch
            {
                return null;
            }
        }

        public async Task<GeneralServiceResponseDto> DeleteExternalProductAsync(int externalProductId)
        {
            var response = await _client.GetFromJsonAsync<GeneralServiceResponseDto>($"products/{externalProductId}");
            return response.IsSucceed ?
                ResponseHelper.CreateResponse(true, 200, "Deleted Successfully") :
                ResponseHelper.CreateResponse(false, 400, "Failed to delete external product");
        }
    }
}