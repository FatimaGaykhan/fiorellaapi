using System;
using FiorellaApi.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FiorellaApi.Services.Interfaces
{
	public interface ICategoryService
	{
        Task<IEnumerable<Category>> GetAllAsync();
        //Task<IEnumerable<CategoryProductVM>> GetAllWithProductCountAsync();
        Task<Category> GetByIdAsync(int id);
        Task<bool> ExistAsync(string name);
        Task CreateAsync(Category category);
        Task DeleteAsync(Category category);
        Task<Category> DetailAsync(int id);
        Task<bool> ExistExceptByIdAsync(int id, string name);
        Task EditAsync(Category category);        
    }
}

