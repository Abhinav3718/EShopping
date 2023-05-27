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
    public static class TypesContextSeedData
    {
        public static async Task SeedTypesData(IMongoCollection<ProductType> mongoCollection)
        {
            bool isDataPresent = (await mongoCollection.FindAsync(type => true)).AnyAsync().Result;

            if(!isDataPresent)
            {
                string filePath = Path.Combine("Data", "SeedData", "types.json");
                var data = await File.ReadAllTextAsync(filePath);

                var typesData = JsonSerializer.Deserialize<IEnumerable<ProductType>>(data);

                if(typesData != null)
                {
                    foreach (var type in typesData)
                    {
                        await mongoCollection.InsertOneAsync(type);
                    }
                }
            }
        }
    }
}
