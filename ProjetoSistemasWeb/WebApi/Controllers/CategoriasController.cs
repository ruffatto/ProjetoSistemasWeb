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
    /// API responsável por fazer a manutenção de Categorias
    /// </summary>

    public class CategoriasController : ApiController
    {
        /// <summary>
        /// Este método retorna uma listagem de todos os Categorias
        /// </summary>
        /// <returns>Não possui retorno</returns>
        public HttpResponseMessage Get()
        {
            try
            {
                List<Categorias> categoriasModel = new List<Categorias>();
                CategoriasRepository categoriasRepository = new CategoriasRepository(ConfigurationManager.ConnectionStrings["conexao"].ToString());
                CategoriasAplication categoriasAplication = new CategoriasAplication(categoriasRepository);

                List<ProjetoSistemasWeb.Domain.Entities.Categorias> categorias = categoriasAplication.ProcurarTodos();
                
                foreach(var cat in categorias)
                {
                    categoriasModel.Add(new Categorias()
                    {
                        Descricao = cat.Descricao,
                        Id = cat.Id
                    });
                }

                return Request.CreateResponse(HttpStatusCode.OK, categoriasModel);
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
        /// Este método retorna um Categorias apartir de seu ID
        /// </summary>
        /// <param name="id">Id relativo a chave de busca para o Categorias</param>
        /// <returns>Retorna um Categorias</returns>
        public HttpResponseMessage Get(Guid id)
        {
            try
            {
                Categorias categoriasModel = null;
                CategoriasRepository categoriasRepository = new CategoriasRepository(ConfigurationManager.ConnectionStrings["conexao"].ToString());
                CategoriasAplication categoriasAplication = new CategoriasAplication(categoriasRepository);

                ProjetoSistemasWeb.Domain.Entities.Categorias categorias = categoriasAplication.Procurar(id);

                if (categorias != null)
                {
                    categoriasModel = new Categorias()
                    {
                        Descricao = categorias.Descricao,
                        Id = categorias.Id
                    };

                    return Request.CreateResponse(HttpStatusCode.OK, categoriasModel);
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

        public HttpResponseMessage Post([FromBody] Categorias categorias)
        {
            try
            {
                //Inclusão do CLiente na base de dados
                //Essa inclusão retorna um Id
                //Id retorna para o requisitante do serviço
                CategoriasRepository categoriasRepository = new CategoriasRepository(ConfigurationManager.ConnectionStrings["conexao"].ToString());
                CategoriasAplication categoriasAplication = new CategoriasAplication(categoriasRepository);

                //Converter o model para uma entidade de dominio

                ProjetoSistemasWeb.Domain.Entities.Categorias categoriasDomain = new ProjetoSistemasWeb.Domain.Entities.Categorias()
                {
                    Id = categorias.Id,
                    Descricao = categorias.Descricao
                };

                categoriasAplication.Inserir(categoriasDomain);

                return Request.CreateResponse(HttpStatusCode.OK, Convert.ToString(categoriasDomain.Id));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [Authorize]
        public HttpResponseMessage Put(Guid id, [FromBody] Categorias categorias)
        {
            try
            {
                //Alterar o CLiente na base de dados
                //Essa alteração retorna um Id
                //Id retorna para o requisitante do serviço
                CategoriasRepository categoriasRepository = new CategoriasRepository(ConfigurationManager.ConnectionStrings["conexao"].ToString());
                CategoriasAplication categoriasAplication = new CategoriasAplication(categoriasRepository);

                //Converter o model para uma entidade de dominio

                ProjetoSistemasWeb.Domain.Entities.Categorias categoriasDomain = new ProjetoSistemasWeb.Domain.Entities.Categorias()
                {
                    Id = categorias.Id,
                    Descricao = categorias.Descricao
                };

                categoriasAplication.Alterar(categoriasDomain);

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
                CategoriasRepository categoriasRepository = new CategoriasRepository(ConfigurationManager.ConnectionStrings["conexao"].ToString());
                CategoriasAplication categoriasAplication = new CategoriasAplication(categoriasRepository);


                var retorno = categoriasAplication.Excluir(id);

                return Request.CreateResponse(HttpStatusCode.OK, Convert.ToString(retorno));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
