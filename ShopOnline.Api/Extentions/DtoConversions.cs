using ShopOnline.Api.Entities;
using ShopOnline.Models.Dtos;

namespace ShopOnline.Api.Extentions
{
    public static class DtoConversions
    {
        public static IEnumerable<ProductDto> ConverToDto(this IEnumerable<Product> products,
                                                           IEnumerable<ProductCategory> productCategories)
        {
            return (from Product in products
                    join ProductCategory in productCategories
                    on Product.CategoryId equals ProductCategory.Id
                    select new ProductDto
                    {
                        Id = Product.Id,
                        Name = Product.Name,
                        Description = Product.Description,
                        ImageUrl = Product.ImageURL,
                        Price = Product.Price,
                        Qty = Product.Qty,
                        CategoryId = Product.CategoryId,
                        CategoryName = ProductCategory.Name
                    }).ToList();
        }
    }
}
