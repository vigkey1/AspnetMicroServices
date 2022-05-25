using CatalogAPI.Entities;
using MongoDB.Driver;

namespace CatalogAPI.Data
{
   public interface  IcatalogContext
    {
        IMongoCollection<Product> Products { get;  }
    }
}
