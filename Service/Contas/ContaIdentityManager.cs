using Microsoft.AspNetCore.Identity;
using Repository.Conta;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Account
{
    public class ContaIdentityManager : IContaIdentityManager
    {
        private IContaRepository _repository { get; set; }

        private SignInManager<Domain.Conta> _signInManager { get; set; }

        public ContaIdentityManager(IContaRepository accountRepository, SignInManager<Domain.Conta> signInManager)
        {
            _repository = accountRepository;
            _signInManager = signInManager;
        }

        public async Task<SignInResult> Login(string email, string password)
        {
            var account = await _repository.GetAccountByEmailPassword(email, password);

            if (account == null)
            {
                return SignInResult.Failed;
            }

            await _signInManager.SignInAsync(account, false);

            return SignInResult.Success;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
