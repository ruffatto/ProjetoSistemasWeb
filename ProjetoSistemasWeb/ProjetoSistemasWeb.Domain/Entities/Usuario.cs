using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoSistemasWeb.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Pessoa { get; set; }

        public Usuario()
        {
            Id = Guid.NewGuid();
        }
        public bool PassswordIsValid(string password)
        {
            return (password == Password);
        }
    }
}
