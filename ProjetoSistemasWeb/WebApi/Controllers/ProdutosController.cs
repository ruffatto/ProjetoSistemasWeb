using ProjetoSistemasWeb.Aplication;
using ProjetoSistemasWeb.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    /// <summary>
    /// API responsável por fazer a manutenção de Produtos
    /// </summary>

    public class ProdutosController : ApiController
    {
        /// <summary>
        /// Este método retorna uma listagem de todos os produtos
        /// </summary>
        /// <returns>Não possui retorno</returns>
        public HttpResponseMessage Get()
        {
            try
            {
                List<Produtos> produtosModel = new List<Produtos>();
                ProdutosRepository produtosRepository = new ProdutosRepository(ConfigurationManager.ConnectionStrings["conexao"].ToString());
                ProdutosAplication produtosAplication = new ProdutosAplication(produtosRepository);

                List<ProjetoSistemasWeb.Domain.Entities.Produtos> produtos = produtosAplication.ProcurarTodos();
                
                foreach(var prod in produtos)
                {
                    produtosModel.Add(new Produtos()
                    {
                        Descricao = prod.Descricao,
                        Id = prod.Id,
                        Codigo = prod.Codigo,
                        Imagem = prod.Imagem,
                        Acessos = prod.Acessos,
                        Preco = prod.Preco,
                        IdCategoria = prod.IdCategoria,
                        DescCat = prod.DescCat
                    });
                }

                return Request.CreateResponse(HttpStatusCode.OK, produtosModel);
            }
            catch (ApplicationException ap)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ap);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        /// Este método retorna um produto apartir de seu ID
        /// </summary>
        /// <param name="id">Id relativo a chave de busca para o produto</param>
        /// <returns>Retorna um Produto</returns>
        public HttpResponseMessage Get(Guid id)
        {
            try
            {
                Produtos produtoModel = null;
                ProdutosRepository produtosRepository = new ProdutosRepository(ConfigurationManager.ConnectionStrings["conexao"].ToString());
                ProdutosAplication produtosAplication = new ProdutosAplication(produtosRepository);

                ProjetoSistemasWeb.Domain.Entities.Produtos produto = produtosAplication.Procurar(id);

                if (produto != null)
                {
                    produtoModel = new Produtos()
                    {
                        Descricao = produto.Descricao,
                        Id = produto.Id,
                        Codigo = produto.Codigo,
                        Imagem = produto.Imagem,
                        Acessos = produto.Acessos,
                        Preco = produto.Preco,
                        IdCategoria = produto.IdCategoria
                    };

                    return Request.CreateResponse(HttpStatusCode.OK, produtoModel);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        public HttpResponseMessage Post([FromBody] Produtos produto)
        {
            try
            {
                //Inclusão do CLiente na base de dados
                //Essa inclusão retorna um Id
                //Id retorna para o requisitante do serviço
                ProdutosRepository produtosRepository = new ProdutosRepository(ConfigurationManager.ConnectionStrings["conexao"].ToString());
                ProdutosAplication produtosAplication = new ProdutosAplication(produtosRepository);

                //Converter o model para uma entidade de dominio

                ProjetoSistemasWeb.Domain.Entities.Produtos produtosDomain = new ProjetoSistemasWeb.Domain.Entities.Produtos()
                {
                    Id = produto.Id,
                    Codigo = produto.Codigo,
                    Descricao = produto.Descricao,
                    Imagem = produto.Imagem,
                    Acessos = produto.Acessos,
                    Preco = produto.Preco,
                    IdCategoria = produto.IdCategoria
                };

                produtosAplication.Inserir(produtosDomain);

                return Request.CreateResponse(HttpStatusCode.OK, Convert.ToString(produtosDomain.Id));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        public HttpResponseMessage Put(Guid id, [FromBody] Produtos produto)
        {
            try
            {
                //Alterar o CLiente na base de dados
                //Essa alteração retorna um Id
                //Id retorna para o requisitante do serviço
                ProdutosRepository produtosRepository = new ProdutosRepository(ConfigurationManager.ConnectionStrings["conexao"].ToString());
                ProdutosAplication produtosAplication = new ProdutosAplication(produtosRepository);

                //Converter o model para uma entidade de dominio

                ProjetoSistemasWeb.Domain.Entities.Produtos produtosDomain = new ProjetoSistemasWeb.Domain.Entities.Produtos()
                {
                    Id = id,
                    Codigo = produto.Codigo,
                    Descricao = produto.Descricao,
                    Imagem = produto.Imagem,
                    Acessos = produto.Acessos,
                    Preco = produto.Preco,
                    IdCategoria = produto.IdCategoria
                };

                produtosAplication.Alterar(produtosDomain);

                return Request.CreateResponse(HttpStatusCode.OK, Convert.ToString(id));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        public HttpResponseMessage Delete(Guid id)
        {
            try
            {
                //Excluir o CLiente na base de dados
                //Essa exclusão retorna um Verdadeiro ou Falso
                ProdutosRepository produtosRepository = new ProdutosRepository(ConfigurationManager.ConnectionStrings["conexao"].ToString());
                ProdutosAplication produtosAplication = new ProdutosAplication(produtosRepository);


                var retorno = produtosAplication.Excluir(id);

                return Request.CreateResponse(HttpStatusCode.OK, Convert.ToString(retorno));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
