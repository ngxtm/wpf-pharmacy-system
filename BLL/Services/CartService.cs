using DAL.Entities;
using DAL.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BLL.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsAsync(int accountId)
        {
            return await _cartRepository.GetByAccountIdAsync(accountId);
        }

        public async Task<CartItem> AddToCartAsync(CartItem cartItem)
        {
            return await _cartRepository.AddAsync(cartItem);
        }

        public async Task UpdateCartItemAsync(CartItem cartItem)
        {
            await _cartRepository.UpdateAsync(cartItem);
        }

        public async Task RemoveFromCartAsync(int cartItemId)
        {
            await _cartRepository.DeleteAsync(cartItemId);
        }

        public async Task ClearCartAsync(int accountId)
        {
            await _cartRepository.ClearByAccountIdAsync(accountId);
        }
    }
} 