using ProjetoSistemasWeb.Domain.Entities;
using ProjetoSistemasWeb.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoSistemasWeb.Aplication
{
    public class LocalizacaoAplication
    {
        private ILocalizacaoRepository localizacaoRepository;

        public LocalizacaoAplication(ILocalizacaoRepository localizacaoRepository)
        {
            this.localizacaoRepository = localizacaoRepository;
        }

        public Guid Inserir(Localizacao localizacao)
        {
            try
            {
                localizacaoRepository.Inserir(localizacao);

                return localizacao.CodigoProduto;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public List<Localizacao> Procurar(Guid codigoProduto)
        {
            try
            {
                if (codigoProduto == Guid.Empty)
                {
                    throw new ApplicationException("O Id deve ser informado!");
                }

                var localizacao = localizacaoRepository.Selecionar(codigoProduto);

                return localizacao;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Localizacao> ProcurarTodos()
        {
            try
            {
                var localizacao = localizacaoRepository.SelecionarTodos();

                return localizacao;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
