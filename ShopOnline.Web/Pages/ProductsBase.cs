using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Interface;

namespace ShopOnline.Web.Pages
{
    public class ProductsBase : ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }

        public string? ErrormMessage { get; set; }

        //Populate main lay out component with all productDtos from entity framework db
        protected override async Task OnInitializedAsync()
        {
            try
            {
                Products = await ProductService.GetItems();

                var shoppingCartItems = await ShoppingCartService.GetItems(HardCodedUser.UserId);

                var totalQty = shoppingCartItems.Sum(x => x.Qty);

                ShoppingCartService.RaiseEventOnShoppingCartChanged(totalQty);
            }
            catch (Exception ex)
            {

                ErrormMessage = ex.Message;
            }
            
        }

        //Seperates product dto by ordered category 
        protected IOrderedEnumerable<IGrouping<int, ProductDto>> GetGroupedByCategory()
        {
            return from product in Products
                   group product by product.CategoryId into prodByCatGroup
                   orderby prodByCatGroup.Key
                   select prodByCatGroup;
        }

        //gets category name of specific productDTO by using categoryId that is a field within it
        protected string GetCategoryName(IGrouping<int, ProductDto> groupedProductDto)
        {
            return groupedProductDto.FirstOrDefault(pg => pg.CategoryId == groupedProductDto.Key).CategoryName;
        }


    }
}
