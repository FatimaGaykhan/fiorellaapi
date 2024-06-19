using System;
using FiorellaApi.DTOs.Blogs;
using FiorellaApi.Models;

namespace FiorellaApi.Services.Interfaces
{
	public interface IBlogService
	{
        Task CreateAsync(Blog blog);
        Task<IEnumerable<Blog>> GetAllAsync();
        Task<Blog> GetByIdAsync(int id);
        Task<Blog> DetailAsync(int id);
        Task DeleteAsync(Blog blog);
        Task EditAsync(Blog blog);

    }
}

