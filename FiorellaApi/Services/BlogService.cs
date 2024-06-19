using System;
using FiorellaApi.Data;
using FiorellaApi.DTOs.Blogs;
using FiorellaApi.Models;
using FiorellaApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiorellaApi.Services
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;

        public BlogService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Blog blog)
        {
            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Blog blog)
        {
            _context.Blogs.Remove(blog);

            await _context.SaveChangesAsync();
        }

        public async Task<Blog> DetailAsync(int id)
        {
            return await _context.Blogs.AsNoTracking().Where(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task EditAsync(Blog blog)
        {
            _context.Blogs.Update(blog);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await _context.Blogs.AsNoTracking().ToListAsync();
        }

        public async Task<Blog> GetByIdAsync(int id)
        {
            return await _context.Blogs.AsNoTracking().Where(m => m.Id == id).FirstOrDefaultAsync();
        }
    }
}

