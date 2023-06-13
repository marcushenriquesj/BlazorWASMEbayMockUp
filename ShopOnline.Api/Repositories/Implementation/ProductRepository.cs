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
            var product = await shopOnlineDbContext.Products.FindAsync(id);
            return product;
        }

        //Repo data set returns all product items
        public async Task<IEnumerable<Product>> GetItems()
        {
            var products = await this.shopOnlineDbContext.Products.ToListAsync();
            return (IEnumerable<Product>)products;
        }

        public async Task<IEnumerable<Product>> GetItemsByCategory(int id)
        {
            var products = (from product in shopOnlineDbContext.Products
                            where product.CategoryId == id
                            select new Product
                            {
                                Id = product.Id,
                                Name = product.Name,
                                Description = product.Description,
                                ImageURL = product.ImageURL,
                                Qty = product.Qty,
                                Price = product.Price,
                                CategoryId = product.CategoryId
                            }).ToList();
            return products;
        }
    }
}
