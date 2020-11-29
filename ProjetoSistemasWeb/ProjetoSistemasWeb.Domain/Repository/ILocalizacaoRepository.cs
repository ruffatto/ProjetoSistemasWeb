using ProjetoSistemasWeb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoSistemasWeb.Domain.Repository
{
    public interface ILocalizacaoRepository
    {
        void Inserir(Localizacao localizacao);
        List<Localizacao> Selecionar(Guid codigoProduto);
        List<Localizacao> SelecionarTodos();
    }
}
