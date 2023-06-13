using Microsoft.EntityFrameworkCore;
using ShopOnline.Api.Data;
using ShopOnline.Api.Entities;
using ShopOnline.Api.Repositories.Interface;

namespace ShopOnline.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopOnlineDbContext shopOnlineDbContext;
        public ProductRepository(ShopOnlineDbContext shopOnlineDbContext)
        {
            this.shopOnlineDbContext = shopOnlineDbContext;
        }

        //Repo data set returns all product categories
        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
            var categories = await this.shopOnlineDbContext.ProductCategories.ToListAsync();
            return (IEnumerable<ProductCategory>)categories;
        }

        //Repo data set return specific category
        public async Task<ProductCategory> GetCategory(int id)
        {
            var category = await shopOnlineDbContext.ProductCategories.SingleOrDefaultAsync(c => c.Id == id);
            return category;
        }

        //Repo data set returns specific product item
        public async Task<Product> GetItem(int id)
        {
            var product = await shopOnlineDbContext.Products
                                .Include(p => p.ProductCategory)
                                .SingleOrDefaultAsync(p => p.Id == id);
            return product;
        }

        //Repo data set returns all product items
        public async Task<IEnumerable<Product>> GetItems()
        {
            var products = await this.shopOnlineDbContext.Products
                                    .Include(p => p.ProductCategory).ToArrayAsync();
                                        
            return (IEnumerable<Product>)products;
        }

        public async Task<IEnumerable<Product>> GetItemsByCategory(int id)
        {
            var products = await this.shopOnlineDbContext.Products
                                   .Include(p => p.ProductCategory)
                                   .Where(p => p.CategoryId == id).ToListAsync();
            return products;
        }
    }
}
