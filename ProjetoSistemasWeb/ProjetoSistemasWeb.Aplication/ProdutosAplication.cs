using ProjetoSistemasWeb.Domain.Entities;
using ProjetoSistemasWeb.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoSistemasWeb.Aplication
{
    public class ProdutosAplication
    {
        private IProdutosRepository produtosRepository;

        public ProdutosAplication(IProdutosRepository produtosRepository)
        {
            this.produtosRepository = produtosRepository;
        }

        public Guid Inserir(Produtos produtos)
        {
            try
            {
                if (string.IsNullOrEmpty(produtos.Descricao))
                {
                    throw new ApplicationException("Descrição não informada!");
                }
                produtos.Id = Guid.NewGuid();

                produtosRepository.Inserir(produtos);

                return produtos.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Guid Alterar(Produtos produtos)
        {
            try
            {
                if (string.IsNullOrEmpty(produtos.Descricao))
                {
                    throw new ApplicationException("Descrição não informada!");
                }

                produtosRepository.Alterar(produtos);

                return produtos.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Excluir(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new ApplicationException("O Id deve ser informado!");
                }

                produtosRepository.Excluir(id);

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Produtos Procurar(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new ApplicationException("O Id deve ser informado!");
                }

                var produtos = produtosRepository.Selecionar(id);

                return produtos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Produtos> ProcurarTodos()
        {
            try
            {
                var produtos = produtosRepository.SelecionarTodos();

                return produtos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Produtos MaisAcessado() {
            try
            {
                var produtos = produtosRepository.SelecionarMaisAcessado();

                return produtos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Produtos MenosAcessado()
        {
            try
            {
                var produtos = produtosRepository.SelecionarMenosAcessado();

                return produtos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
