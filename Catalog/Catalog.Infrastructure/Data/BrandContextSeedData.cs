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
    public static class BrandContextSeedData
    {
        public static async Task SeedBrandsData(IMongoCollection<ProductBrand> mongoCollection)
        {
            Task<bool> isDataPresentInSeedJson = (await mongoCollection.FindAsync(brand => true)).AnyAsync();

            if(!isDataPresentInSeedJson.Result)
            {
                string filePath = Path.Combine("Data", "SeedData", "brands.json");
                var brandsData = await File.ReadAllTextAsync(filePath);
                
                var data = JsonSerializer.Deserialize<IEnumerable<ProductBrand>>(brandsData);

                if(data != null)
                {
                    foreach( var item in data)
                    {
                        await mongoCollection.InsertOneAsync(item);
                    }
                }
            }
        }
    }
}
