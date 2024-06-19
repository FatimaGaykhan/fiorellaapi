using System;
using System.Reflection.Metadata;
using FiorellaApi.Data;
using FiorellaApi.Models;
using FiorellaApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiorellaApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();
        }

        public Task<Category> DetailAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task EditAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(string name)
        {
            return await _context.Categories.AsNoTracking().AnyAsync(m => m.Name.Trim() == name.Trim());
        }

        public async Task<bool> ExistExceptByIdAsync(int id, string name)
        {
            return await _context.Categories.AsNoTracking().AnyAsync(m => m.Name == name && m.Id != id);

        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.AsNoTracking().Where(m => m.Id == id).FirstOrDefaultAsync();
        }
    }
}

