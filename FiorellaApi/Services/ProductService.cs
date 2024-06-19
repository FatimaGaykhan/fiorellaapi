using System;
using System.Reflection.Metadata;
using FiorellaApi.Data;
using FiorellaApi.Models;
using FiorellaApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiorellaApi.Services
{
	public class ProductService:IProductService
	{
        private readonly AppDbContext _context;

		public ProductService(AppDbContext context)
		{
            _context = context;
		}

        public async Task CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);

            await _context.SaveChangesAsync();
        }

        public async Task<Product> DetailAsync(int id)
        {
            return await _context.Products.Include(m => m.ProductImages).Include(m=>m.Category).AsNoTracking().Where(m => m.Id == id).FirstOrDefaultAsync();

        }

        public async Task EditAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistExceptByIdAsync(int id, string name)
        {
            return await _context.Products.AsNoTracking().AnyAsync(m => m.Name == name && m.Id != id);

        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.Include(m=>m.ProductImages).Include(m=>m.Category).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllWithImagesAsync()
        {
            return await _context.Products.Include(m => m.ProductImages).AsNoTracking().ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.Include(m=>m.ProductImages).AsNoTracking().Where(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Product> GetByIdWithAllDatas(int id)
        {
            return await _context.Products.AsNoTracking().Where(m => m.Id == id)
                                          .Include(m => m.Category)
                                          .Include(m => m.ProductImages)
                                          .FirstOrDefaultAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Products.AsNoTracking().CountAsync();
        }

        public async Task<ProductImage> GetProductImageByIdAsync(int? id)
        {
            return await _context.ProductImages.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task ImageDeleteAsync(ProductImage image)
        {
            _context.ProductImages.Remove(image);
            await _context.SaveChangesAsync();
        }

        //public async Task IsMainAsync()
        //{
        //    _context.Products.Update();
        //    await _context.SaveChangesAsync();
        //}
    }
}

