using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain
{
    public class Livro
    {
        public Guid Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public string Ano { get; set; }

        [JsonIgnore]
        public Autor Autor { get; set; }
    }
}
