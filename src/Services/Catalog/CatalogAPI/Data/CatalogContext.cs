using CatalogAPI.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CatalogAPI.Data
{
    public class CatalogContext : IcatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var Database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            Products = Database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogContextSeed.SeedData(Products); 
        }
        public IMongoCollection<Product> Products { get; }
    }
}
