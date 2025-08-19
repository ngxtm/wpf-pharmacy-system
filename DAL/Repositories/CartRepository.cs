using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;

namespace DAL.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly PharmacyDbContext _context;

        public CartRepository(PharmacyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartItem>> GetByAccountIdAsync(int accountId)
        {
            return await _context.CartItems
                .Where(c => c.AccountId == accountId && c.IsActive)
                .Include(c => c.Product)
                .OrderBy(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<CartItem> AddAsync(CartItem cartItem)
        {
            cartItem.CreatedAt = DateTime.Now;
            cartItem.UpdatedAt = DateTime.Now;
            cartItem.IsActive = true;
            
            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }

        public async Task UpdateAsync(CartItem cartItem)
        {
            cartItem.UpdatedAt = DateTime.Now;
            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                cartItem.IsActive = false;
                cartItem.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearByAccountIdAsync(int accountId)
        {
            var cartItems = await _context.CartItems
                .Where(c => c.AccountId == accountId && c.IsActive)
                .ToListAsync();
                
            foreach (var item in cartItems)
            {
                item.IsActive = false;
                item.UpdatedAt = DateTime.Now;
            }
            
            await _context.SaveChangesAsync();
        }
    }
} 