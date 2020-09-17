using Context.Repository;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.Conta
{
    public class ContaRepository : IUserStore<Domain.Conta>, IContaRepository
    {

        private BibliotecaContext _bibliotecaContext;

        public ContaRepository(BibliotecaContext bibliotecaContext)
        {
            _bibliotecaContext = bibliotecaContext;
        }

        public async Task<IdentityResult> CreateAsync(Domain.Conta user, CancellationToken cancellationToken)
        {
            _bibliotecaContext.Contas.Add(user);
            await _bibliotecaContext.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(Domain.Conta user, CancellationToken cancellationToken)
        {
            _bibliotecaContext.Contas.Remove(user);
            await _bibliotecaContext.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public Task<Domain.Conta> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return _bibliotecaContext.Contas.FirstOrDefaultAsync(x => x.Id == new Guid(userId));
        }

        public Task<Domain.Conta> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return _bibliotecaContext.Contas.FirstOrDefaultAsync(x => x.Email == normalizedUserName);
        }

        public Task<Domain.Conta> GetAccountByEmailPassword(string email, string password)
        {
            return Task.FromResult(_bibliotecaContext.Contas
                .Include(x => x.Perfil)
                .FirstOrDefault(x => x.Email == email && x.Password == password));
        }

        public Task<string> GetNormalizedUserNameAsync(Domain.Conta user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<string> GetUserIdAsync(Domain.Conta user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(Domain.Conta user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task SetNormalizedUserNameAsync(Domain.Conta user, string normalizedName, CancellationToken cancellationToken)
        {
            user.Email = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(Domain.Conta user, string userName, CancellationToken cancellationToken)
        {
            user.Email = userName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(Domain.Conta user, CancellationToken cancellationToken)
        {
            var contaToUpdate = await _bibliotecaContext.Contas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == user.Id);

            contaToUpdate = user;
            _bibliotecaContext.Entry(contaToUpdate).State = EntityState.Modified;

            _bibliotecaContext.Contas.Add(contaToUpdate);
            await _bibliotecaContext.SaveChangesAsync();

            return IdentityResult.Success;
        }

        private bool disposedValue = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ContaRepository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
