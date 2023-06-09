using ShopOnline.Models.Dtos;

namespace ShopOnline.Web.Services.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetItems();
        Task<ProductDto> GetItem(int id);
    }
}
