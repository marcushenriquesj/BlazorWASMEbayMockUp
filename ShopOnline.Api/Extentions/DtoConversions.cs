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

        public static ProductDto ConverToDto(this Product product,
                                            ProductCategory productCategory)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageURL,
                Price = product.Price,
                Qty = product.Qty,
                CategoryId = product.CategoryId,
                CategoryName = productCategory.Name

            };
        }

        public static IEnumerable<CartItemDto> ConvertToDto(this IEnumerable<CartItem> cartItems,
                                                            IEnumerable<Product> products)
        {
            return (from cartItem in cartItems
                    join product in products
                    on cartItem.ProductId equals product.Id
                    select new CartItemDto
                    {
                        Id = cartItem.Id,
                        ProductID = cartItem.ProductId,
                        ProductName = product.Name,
                        ProductDescription = product.Description,
                        ProductImageURL = product.ImageURL,
                        Price = product.Price,
                        CartId = cartItem.CartId,
                        Qty = cartItem.Qty,
                        TotalPrice = product.Price * cartItem.Qty
                    }).ToList();
        }

        public static CartItemDto ConvertToDto(this CartItem cartItem,
                                               Product product)
        {
            return new CartItemDto
                    {
                        Id = cartItem.Id,
                        ProductID = cartItem.ProductId,
                        ProductName = product.Name,
                        ProductDescription = product.Description,
                        ProductImageURL = product.ImageURL,
                        Price = product.Price,
                        CartId = cartItem.CartId,
                        Qty = cartItem.Qty,
                        TotalPrice = product.Price * cartItem.Qty
                    };
        }


    }
}
