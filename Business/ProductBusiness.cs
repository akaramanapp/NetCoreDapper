using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using netcoreDapper.Business;
using netcoreDapper.Contracts;
using netcoreDapper.Data;
using netcoreDapper.Models;

namespace netcoreDapper.Business
{
    public class ProductBusiness : IProductBusiness
    {
        private readonly IProductRepository _productRepository;

        public ProductBusiness(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductResponse> GetAsync(long id)
        {
            ProductResponse productResponse = new ProductResponse();
            var product = await _productRepository.GetAsync(id);

            if(product == null)
            {
                productResponse.Message = "Product not found.";
            }
            else
            {
                productResponse.Products.Add(product);
            }

            return productResponse;
        }

        public async Task<ProductResponse> GetAllAsync()
        {
            //TODO: Paging...

            ProductResponse productResponse = new ProductResponse();
            IEnumerable<Product> products = await _productRepository.GetAllAsync();
            
            if(products.ToList().Count == 0)
            {
                productResponse.Message = "Products not found.";
            }
            else
            {
                productResponse.Products.AddRange(products);
            }

            return productResponse;
        }

        public async Task AddAsync(ProductRequest productRequest)
        {
            Product product = new Product(){
                CategoryId = productRequest.CategoryId,
                Name = productRequest.Name,
                Description = productRequest.Description,
                Price = productRequest.Price,
                CreatedDate = DateTime.Now
            };

            await _productRepository.AddAsync(product);
        }
    }
}