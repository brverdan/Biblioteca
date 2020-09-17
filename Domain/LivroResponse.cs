using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain
{
    public class LivroResponse
    {
        public Guid Id { get; set; }

        public string Titulo { get; set; }

        public string ISBN { get; set; }

        public string Ano { get; set; }


        public Autor Autor { get; set; }
    }
}
