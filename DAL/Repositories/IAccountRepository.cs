using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Repositories
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAllAsync();
        Task<Account?> GetByIdAsync(int id);
        Task<Account?> GetByUsernameAsync(string username);
        Task<Account> AddAsync(Account account);
        Task<Account> UpdateAsync(Account account);
        Task DeleteAsync(int id);
        Task<bool> ValidateLoginAsync(string username, string password);
        Task<bool> UpdateBalanceAsync(int accountId, decimal newBalance);
    }
} 