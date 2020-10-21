using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CategoriasController : ApiController
    {
        public HttpResponseMessage Get()
        {
            List<Categorias> categorias = new List<Categorias>();

            try
            {
                categorias.Add(new Categorias()
                {
                    Id = Guid.NewGuid(),
                    Descricao = "Cama"

                });

                categorias.Add(new Categorias()
                {
                    Id = Guid.NewGuid(),
                    Descricao = "Sofá"

                });

                categorias.Add(new Categorias()
                {
                    Id = Guid.NewGuid(),
                    Descricao = "Mesa"

                });

                return Request.CreateResponse(HttpStatusCode.OK, categorias);
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
                Categorias categori = new Categorias()
                {
                    Descricao = "Cama",
                    Id = id
                };

                return Request.CreateResponse(HttpStatusCode.OK, categori);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        public HttpResponseMessage Post([FromBody] Categorias categoria)
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

        public HttpResponseMessage Put(Guid id, [FromBody] Categorias categoria)
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
