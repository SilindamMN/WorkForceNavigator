using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Shop
{
  public class ProductDto
{
    public int Id { get; set; }   // Internal Product Id
        public int ExternalProductId { get; set; } // FakeStore API Id

        public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Category { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}

}
