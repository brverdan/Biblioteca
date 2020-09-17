using Context.Repository;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.Conta
{
    public class PerfilRepository : IRoleStore<Perfil>
    {
        private BibliotecaContext _bibliotecaContext;

        public PerfilRepository(BibliotecaContext bibliotecaContext)
        {
            _bibliotecaContext = bibliotecaContext;
        }

        public async Task<IdentityResult> CreateAsync(Perfil role, CancellationToken cancellationToken)
        {
            _bibliotecaContext.Perfis.Add(role);
            await _bibliotecaContext.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(Perfil role, CancellationToken cancellationToken)
        {
            _bibliotecaContext.Perfis.Remove(role);
            await _bibliotecaContext.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<Perfil> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return await _bibliotecaContext.Perfis.FirstOrDefaultAsync(x => x.Id == new Guid(roleId));
        }

        public async Task<Perfil> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return await _bibliotecaContext.Perfis.FirstOrDefaultAsync(x => x.Nome == normalizedRoleName);
        }

        public Task<string> GetNormalizedRoleNameAsync(Perfil role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Nome);
        }

        public Task<string> GetRoleIdAsync(Perfil role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(Perfil role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Nome);
        }

        public Task SetNormalizedRoleNameAsync(Perfil role, string normalizedName, CancellationToken cancellationToken)
        {
            role.Nome = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetRoleNameAsync(Perfil role, string roleName, CancellationToken cancellationToken)
        {
            role.Nome = roleName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(Perfil role, CancellationToken cancellationToken)
        {
            var perfilToUpdate = await _bibliotecaContext.Perfis.AsNoTracking().FirstOrDefaultAsync(x => x.Id == role.Id);

            perfilToUpdate = role;
            _bibliotecaContext.Entry(perfilToUpdate).State = EntityState.Modified;

            _bibliotecaContext.Perfis.Add(perfilToUpdate);
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
        // ~PerfilRepository()
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
