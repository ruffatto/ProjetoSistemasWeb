using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ProdutosController : ApiController
    {
        public HttpResponseMessage Get()
        {
            List<Produtos> produtos = new List<Produtos>();

            try
            {
                produtos.Add(new Produtos()
                {
                    Codigo = 1,
                    Descricao = "Produto 1",
                    Imagem = "gs://leitura-9ce08.appspot.com/Imagens/Busca de Imagem.png",
                    Id = Guid.NewGuid(),
                    Acessos = 0,
                    Preco = 100,
                    Categoria = new Categorias()
                    {
                        Descricao = "Cama",
                        Id = Guid.NewGuid()
                    }
                });

                produtos.Add(new Produtos()
                {
                    Codigo = 2,
                    Descricao = "Produto 2",
                    Imagem = "gs://leitura-9ce08.appspot.com/Imagens/Busca de Imagem.png",
                    Id = Guid.NewGuid(),
                    Acessos = 0,
                    Preco = 200,
                    Categoria = new Categorias()
                    {
                        Descricao = "Sofá",
                        Id = Guid.NewGuid()
                    }
                });

                produtos.Add(new Produtos()
                {
                    Codigo = 3,
                    Descricao = "Produto 3",
                    Imagem = "gs://leitura-9ce08.appspot.com/Imagens/Busca de Imagem.png",
                    Id = Guid.NewGuid(),
                    Acessos = 0,
                    Preco = 300,
                    Categoria = new Categorias()
                    {
                        Descricao = "Mesa",
                        Id = Guid.NewGuid()
                    }
                });

                return Request.CreateResponse(HttpStatusCode.OK, produtos);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        public HttpResponseMessage Get(Guid id)
        {
            try
            {
                Produtos prod = new Produtos()
                {
                    Codigo = 1,
                    Descricao = "Produto 1",
                    Imagem = "gs://leitura-9ce08.appspot.com/Imagens/Busca de Imagem.png",
                    Id = id,
                    Acessos = 0,
                    Preco = 100,
                    Categoria = new Categorias()
                    {
                        Descricao = "Cama",
                        Id = Guid.NewGuid()
                    }
                };

                return Request.CreateResponse(HttpStatusCode.OK, prod);
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
                Guid id = Guid.NewGuid();
                return Request.CreateResponse(HttpStatusCode.OK, Convert.ToString(id));
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
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
