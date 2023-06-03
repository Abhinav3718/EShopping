using Catalog.Core.Entities;
using Catalog.Infrastructure.Data.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; private set; }

        public IMongoCollection<ProductBrand> Brands { get; private set; }

        public IMongoCollection<ProductType> Types { get; private set; }

        private IConfiguration _configuration;

        public CatalogContext(IConfiguration configuration)
        {
            _configuration = configuration;

            var connectionString = GetConfigurationValue("DbSettings:ConnectionString");
            var dbName = GetConfigurationValue("DbSettings:DatabaseName");

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(dbName);

            var brandsKey = GetConfigurationValue("DbSettings:BrandsCollection");
            var productsKey = GetConfigurationValue("DbSettings:ProductsCollection");
            var typesKey = GetConfigurationValue("DbSettings:TypesCollection");

            SetCollections(database, brandsKey, productsKey, typesKey);

            SeedData();

        }
        private string GetConfigurationValue(string key)
        {
            return _configuration.GetValue<string>(key);
        }

        private void SetCollections(IMongoDatabase database, string? brandsKey = null, string? productsKey = null, string? typesKey = null)
        {
            if(brandsKey != null)
            {
                Brands = database.GetCollection<ProductBrand>(brandsKey);
            }
            if (productsKey != null)
            {
                Products = database.GetCollection<Product>(productsKey);
            }
            if (typesKey != null)
            {
                Types = database.GetCollection<ProductType>(typesKey);
            }
        }

        private async void SeedData()
        {
            if(Brands != null)
            {
                await BrandContextSeedData.SeedBrandsData(Brands);
            }
            if(Products != null)
            {
                await CatalogContextSeedData.SeedProductsData(Products);
            }
            if(Types != null)
            {
                await TypesContextSeedData.SeedTypesData(Types);
            }
        }
    }
}
