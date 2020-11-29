using ProjetoSistemasWeb.Domain.Entities;
using ProjetoSistemasWeb.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoSistemasWeb.Aplication
{
    public class CategoriasAplication
    {
        private ICategoriasRepository categoriasRepository;

        public CategoriasAplication(ICategoriasRepository categoriasRepository)
        {
            this.categoriasRepository = categoriasRepository;
        }

        public Guid Inserir(Categorias categorias)
        {
            try
            {
                if (string.IsNullOrEmpty(categorias.Descricao))
                {
                    throw new ApplicationException("Descrição não informada!");
                }
                categorias.Id = Guid.NewGuid();

                categoriasRepository.Inserir(categorias);

                return categorias.Id;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public Guid Alterar(Categorias categorias)
        {
            try
            {
                if (string.IsNullOrEmpty(categorias.Descricao))
                {
                    throw new ApplicationException("Descrição não informada!");
                }

                categoriasRepository.Alterar(categorias);

                return categorias.Id;
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

                categoriasRepository.Excluir(id);

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Categorias Procurar(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new ApplicationException("O Id deve ser informado!");
                }

                var categorias = categoriasRepository.Selecionar(id);

                return categorias;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Categorias> ProcurarTodos()
        {
            try
            {
                var categorias = categoriasRepository.SelecionarTodos();

                return categorias;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
