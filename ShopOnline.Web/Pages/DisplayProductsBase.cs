using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;

namespace ShopOnline.Web.Pages
{
    public class DisplayProductsBase : ComponentBase
    {
        //made display of all products a seperate component for reuse later.
        [Parameter]
        public IEnumerable<ProductDto> Products { get; set; }

    }
}
