using DAL.Entities;
using DAL.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BLL.Services
{
    public interface ICartService
    {
        Task<IEnumerable<CartItem>> GetCartItemsAsync(int accountId);
        Task<CartItem> AddToCartAsync(CartItem cartItem);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task RemoveFromCartAsync(int cartItemId);
        Task ClearCartAsync(int accountId);
    }
} 