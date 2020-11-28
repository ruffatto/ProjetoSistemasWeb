using ProjetoSistemasWeb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoSistemasWeb.Domain.Repository
{
    public interface IUsuarioRepository
    {
        Guid Insert(Usuario usuario);
        Usuario Find(Guid id);
        Usuario Find(string email);
        List<Usuario> FindAll();
        Guid Update(Usuario usuario);
        bool Delete(Guid id);
    }
}
