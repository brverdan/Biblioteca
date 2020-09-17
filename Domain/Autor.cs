using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Autor
    {
        public Guid Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Sobrenome { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Digite um email válido.")]
        public string Email { get; set; }


        [Required]
        public DateTime DtAniversario { get; set; }

        public List<Livro> Livros { get; set; }
    }
}
