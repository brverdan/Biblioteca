using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Perfil
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public List<Conta> Contas { get; set; }
    }
}
