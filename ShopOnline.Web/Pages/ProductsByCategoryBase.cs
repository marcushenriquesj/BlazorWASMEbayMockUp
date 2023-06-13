using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Interface;

namespace ShopOnline.Web.Pages
{
    public class ProductsByCategoryBase:ComponentBase
    {
        [Parameter]
        public int CategoryId { get; set; }
        [Inject]
        public IProductService ProductService { get; set; }

        public IEnumerable<ProductDto> Products { get; set; }
        public string CategoryName { get; set; }    
        public string ErrorMessage { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                Products = await ProductService.GetItemsByCategory(CategoryId);
                if(Products != null && Products.Count() > 0)
                {
                    var productdto = Products.FirstOrDefault(p => p.CategoryId == CategoryId);
                    if(productdto != null)
                    {
                        CategoryName = productdto.CategoryName;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
