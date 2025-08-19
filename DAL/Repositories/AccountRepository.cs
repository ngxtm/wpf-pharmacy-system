using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace DAL.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly PharmacyDbContext _context;

        public AccountRepository(PharmacyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _context.Accounts
                .Where(a => a.IsActive)
                .OrderBy(a => a.Username)
                .ToListAsync();
        }

        public async Task<Account?> GetByIdAsync(int id)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(a => a.Id == id && a.IsActive);
        }

        public async Task<Account?> GetByUsernameAsync(string username)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(a => a.Username == username && a.IsActive);
        }

        public async Task<Account> AddAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<Account> UpdateAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task DeleteAsync(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                account.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ValidateLoginAsync(string username, string password)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Username == username && a.Password == password && a.IsActive);
            return account != null;
        }

        public async Task<bool> UpdateBalanceAsync(int accountId, decimal newBalance)
        {
            var account = await _context.Accounts.FindAsync(accountId);
            if (account != null)
            {
                account.Balance = newBalance;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
} 