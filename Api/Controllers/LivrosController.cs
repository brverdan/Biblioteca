using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Context.Repository;
using Domain;

namespace Api.Controllers
{
    [Route("api/Livros")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly BibliotecaContext _context;

        public LivrosController(BibliotecaContext context)
        {
            _context = context;
        }

        // GET: api/Livros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livro>>> GetLivros()
        {
            return await _context.Livros.ToListAsync();
        }

        // GET: api/Livros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LivroResponse>> GetLivro([FromRoute]Guid id)
        {

            var livro = await _context.Livros.Include(x => x.Autor).FirstOrDefaultAsync(x => x.Id == id);

            if (livro == null)
            {
                return NotFound();
            }

            LivroResponse livroa = new LivroResponse { Id = livro.Id, Ano = livro.Ano, Autor = livro.Autor, ISBN = livro.ISBN, Titulo = livro.Titulo };

            return livroa;
        }

        // PUT: api/Livros/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLivro([FromRoute] Guid id, [FromBody] LivroResponse livro)
        {
            livro.Autor = _context.Autores.FirstOrDefault(x => x.Id == livro.Autor.Id);
            Livro livroa = new Livro { Id = livro.Id ,Ano = livro.Ano, Autor = livro.Autor, ISBN = livro.ISBN, Titulo = livro.Titulo };
            if (id != livroa.Id)
            {
                return BadRequest();
            }

            _context.Entry(livroa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Livros
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Livro>> PostLivro(LivroResponse livro)
        {
            livro.Autor = _context.Autores.FirstOrDefault(x => x.Id == livro.Autor.Id);
            Livro livroa = new Livro { Ano = livro.Ano, Autor = livro.Autor, ISBN = livro.ISBN, Titulo = livro.Titulo};
            _context.Livros.Add(livroa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLivro", new { id = livro.Id }, livro);
        }

        // DELETE: api/Livros/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Livro>> DeleteLivro([FromRoute]Guid id)
        {
            var livro = await _context.Livros.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }

            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();

            return livro;
        }

        private bool LivroExists(Guid id)
        {
            return _context.Livros.Any(e => e.Id == id);
        }
    }
}
