using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using netcoreDapper.Contracts;
using netcoreDapper.Models;

namespace netcoreDapper.Business
{
    public interface IProductBusiness
    {
        Task<ProductResponse> GetAsync(long id);
        Task<ProductResponse> GetAllAsync();
        Task AddAsync(ProductRequest productRequest);
    }
}