using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Autor
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }

        [EmailAddress(ErrorMessage = "Digite um email válido.")]
        public string Email { get; set; }

        public DateTime DtAniversario { get; set; }

        public List<Livro> Livros { get; set; }
    }
}
