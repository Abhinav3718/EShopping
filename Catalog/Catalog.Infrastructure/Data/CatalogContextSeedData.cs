using Catalog.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data
{
    public static class CatalogContextSeedData
    {
        public static async Task SeedProductsData(IMongoCollection<Product> mongoCollection)
        {
            bool isDataPresent = (await mongoCollection.FindAsync(type => true)).AnyAsync().Result;

            if (!isDataPresent)
            {
                string filePath = Path.Combine("Data", "SeedData", "products.json");
                var data = await File.ReadAllTextAsync(filePath);

                var productsData = JsonSerializer.Deserialize<IEnumerable<Product>>(data);

                if (productsData != null)
                {
                    foreach (var type in productsData)
                    {
                        await mongoCollection.InsertOneAsync(type);
                    }
                }
            }
        }
    }
}
