using Newtonsoft.Json;
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Interface;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace ShopOnline.Web.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly HttpClient _httpClient;
        public event Action<int> OnShoppingCartChanged;

        public ShoppingCartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }        

        //Http Post: adds product to user's cart
        public async Task<CartItemDto> AddItem(CartItemToAddDto cartItemToAddDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<CartItemToAddDto>($"api/ShoppingCart", cartItemToAddDto);
                if(response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(CartItemDto);
                    }
                    return await response.Content.ReadFromJsonAsync<CartItemDto>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status: {response.StatusCode} Message -{message}");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }
        
        //Http Delete: deletes item from user's shopping cart based on cartItemDto Id
        public async Task<CartItemDto> DeleteItem(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/ShoppingCart/{id}");
                if(response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CartItemDto>();
                }
                return default(CartItemDto);
               
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        //Http Get receives all items within a user's shopping cart in the form of CartItemDto
        public async Task<List<CartItemDto>> GetItems(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/ShoppingCart/{userId}/GetItems");
                if(response.IsSuccessStatusCode)
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<CartItemDto>().ToList();
                    }
                    return await response.Content.ReadFromJsonAsync<List<CartItemDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status coude: {response.StatusCode} - {message}");
                }
            }
            catch (Exception)
            {
                //Log Exception
                throw;
            }
        }

        //Event handler for CartMenu Component: when item Qty is changed event is raised
        public void RaiseEventOnShoppingCartChanged(int totalQty)
        {
            //Check if event has subs
            if(OnShoppingCartChanged != null)
            {
                //Raise to subs
                OnShoppingCartChanged.Invoke(totalQty);
            }
        }

        //Create json request for Http Patch async controller call to update quanity of items in a user's shopping cart
        public async Task<CartItemDto> UpdateQty(CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(cartItemQtyUpdateDto);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");

                var response = await _httpClient.PatchAsync($"api/ShoppingCart/{cartItemQtyUpdateDto.CartItemId}", content);

                if(response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CartItemDto>();
                }
                return null;
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }
    }
}
