using CatalogAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogAPI.Repositories
{
    public interface  IProductRepo
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(string id);
        Task<IEnumerable<Product>> GetProductByName(string name);
        Task<IEnumerable<Product>> GetProductByCategory(string CategoryName);
        Task CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);


    }
}
