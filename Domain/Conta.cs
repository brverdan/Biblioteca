using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Conta
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Perfil Perfil{ get; set; }
    }
}
