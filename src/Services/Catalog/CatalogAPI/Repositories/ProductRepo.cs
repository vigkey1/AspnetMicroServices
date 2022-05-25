using CatalogAPI.Data;
using CatalogAPI.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogAPI.Repositories
{
    public class ProductRepo : IProductRepo
    {
        private readonly IcatalogContext _context;
        public ProductRepo(IcatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

        }
        public async Task CreateProduct(Product product)
        {
           await _context.Products.InsertOneAsync(product);
      
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);


            DeleteResult deleteResult = await _context.Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _context
                           .Products
                           .Find(x => x.Id == id)
                           .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string CategoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, CategoryName);


            return await _context
                                     .Products
                                     .Find(filter)
                                     .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);


            return await _context
                                     .Products
                                     .Find(filter)
                                     .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context
                .Products
                .Find(x => true)
                .ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _context.Products.ReplaceOneAsync(g=>g.Id == product.Id, product);
            
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0 ;

        }
    }
}
