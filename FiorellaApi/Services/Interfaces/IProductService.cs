using System;
using FiorellaApi.Models;

namespace FiorellaApi.Services.Interfaces
{
	public interface IProductService
	{
        Task<IEnumerable<Product>> GetAllWithImagesAsync();
        Task<Product> GetByIdWithAllDatas(int id);
        Task<Product> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync();
        //Task<IEnumerable<Product>> GetAllPaginateAsync(int page, int take);
        //IEnumerable<ProductVM> GetMappedDatas(IEnumerable<Product> products);
        Task<int> GetCountAsync();
        Task CreateAsync(Product product);
        Task DeleteAsync(Product product);
        Task<bool> ExistExceptByIdAsync(int id, string name);
        Task EditAsync(Product product);
        Task<ProductImage> GetProductImageByIdAsync(int? id);
        Task ImageDeleteAsync(ProductImage image);
        Task<Product> DetailAsync(int id);
        //Task IsMainAsync();


    }
}

