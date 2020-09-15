using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain
{
    public class Livro
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string ISBN { get; set; }
        public string Ano { get; set; }

        [JsonIgnore]
        public Autor Autor { get; set; }
    }
}
