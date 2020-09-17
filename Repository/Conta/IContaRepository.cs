using Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.Conta
{
    public interface IContaRepository
    {
        Task<Domain.Conta> GetAccountByEmailPassword(string email, string password);
        Task<Domain.Conta> FindByIdAsync(string userId, CancellationToken cancellationToken);
    }
}
