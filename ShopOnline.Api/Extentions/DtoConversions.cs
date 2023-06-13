using ShopOnline.Api.Entities;
using ShopOnline.Models.Dtos;

namespace ShopOnline.Api.Extentions
{
    public static class DtoConversions
    {
        //Converts Ienum of products and product categories to combo obj
        public static IEnumerable<ProductDto> ConverToDto(this IEnumerable<Product> products)
        {
            return (from Product in products
                    select new ProductDto
                    {
                        Id = Product.Id,
                        Name = Product.Name,
                        Description = Product.Description,
                        ImageUrl = Product.ImageURL,
                        Price = Product.Price,
                        Qty = Product.Qty,
                        CategoryId = Product.ProductCategory.Id,
                        CategoryName = Product.ProductCategory.Name
                    }).ToList();
        }

        //Converts single product and product category to combo obj
        public static ProductDto ConverToDto(this Product product)
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
                CategoryName = product.ProductCategory.Name

            };
        }

        //Converts Ienum of products and and cart items (place holder that relations user's cart and product/Qty) to combo obj
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

        //Converts single product and and cart item (place holder that relations user's cart and product/Qty) to combo obj
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

        public static IEnumerable<ProductCategoryDto> ConvertToDto(this IEnumerable<ProductCategory> productCategories)
        {
            return (from productCategory in productCategories
                    select new ProductCategoryDto
                    {
                        Id= productCategory.Id,
                        Name= productCategory.Name,
                        IconCss = productCategory.IconCss
                    }).ToList();
        }



    }
}
