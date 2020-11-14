using ProjetoSistemasWeb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoSistemasWeb.Domain.Repository
{
    public interface IProdutosRepository
    {
        void Inserir(Produtos produtos);
        void Excluir(Guid id);
        void Alterar(Produtos produtos);
        Produtos Selecionar(Guid id);
        List<Produtos> SelecionarTodos();
    }
}
