using Context.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Autor
{
    public class AutorRepository
    {
        private readonly BibliotecaContext _context;

        public AutorRepository(BibliotecaContext context)
        {
            _context = context;
        }
        public async Task<Domain.Autor> FindById(Guid id)
        {
            return await _context.Autores.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<Domain.Autor>> Autores()
        {
            return await _context.Autores.ToListAsync();
        }
    }
}
