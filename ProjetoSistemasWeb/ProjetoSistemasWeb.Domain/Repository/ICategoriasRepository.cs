using ProjetoSistemasWeb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoSistemasWeb.Domain.Repository
{
    public interface ICategoriasRepository
    {
        void Inserir(Categorias categorias);
        void Excluir(Guid id);
        void Alterar(Categorias categorias);
        Categorias Selecionar(Guid id);
        List<Categorias> SelecionarTodos();
    }
}
