using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAccountRepository _accountRepository;

        public AuthService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<bool> ValidateLoginAsync(string username, string password)
        {
            return await _accountRepository.ValidateLoginAsync(username, password);
        }

        public async Task<Account?> GetAccountByUsernameAsync(string username)
        {
            return await _accountRepository.GetByUsernameAsync(username);
        }

        public async Task<Account> CreateAccountAsync(Account account)
        {
            return await _accountRepository.AddAsync(account);
        }

        public async Task<bool> UpdateBalanceAsync(int accountId, decimal newBalance)
        {
            return await _accountRepository.UpdateBalanceAsync(accountId, newBalance);
        }

        public async Task<Account?> GetAccountByIdAsync(int id)
        {
            return await _accountRepository.GetByIdAsync(id);
        }
    }
} 