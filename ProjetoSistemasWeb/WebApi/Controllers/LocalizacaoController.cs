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
    /// API responsável por fazer a manutenção de Localizacao
    /// </summary>

    public class LocalizacaoController : ApiController
    {
        /// <summary>
        /// Este método retorna uma listagem de todos os Localizacao
        /// </summary>
        /// <returns>Não possui retorno</returns>
        public HttpResponseMessage Get()
        {
            try
            {
                List<Localizacao> localizacaoModel = new List<Localizacao>();
                LocalizacaoRepository localizacaoRepository = new LocalizacaoRepository(ConfigurationManager.ConnectionStrings["conexao"].ToString());
                LocalizacaoAplication localizacaoAplication = new LocalizacaoAplication(localizacaoRepository);

                List<ProjetoSistemasWeb.Domain.Entities.Localizacao> localizacao = localizacaoAplication.ProcurarTodos();

                foreach (var loc in localizacao)
                {
                    localizacaoModel.Add(new Localizacao()
                    {
                        CodigoProduto = loc.CodigoProduto,
                        Data = loc.Data,
                        Lat = loc.Lat,
                        Long = loc.Long
                    });
                }

                return Request.CreateResponse(HttpStatusCode.OK, localizacaoModel);
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
        /// Este método retorna um Localizacao apartir de seu ID
        /// </summary>
        /// <param name="id">Id relativo a chave de busca para o Localizacao</param>
        /// <returns>Retorna um Localizacao</returns>
        public HttpResponseMessage Get(Guid codigoProduto)
        {
            try
            {

                List<Localizacao> localizacaoModel = new List<Localizacao>();
                LocalizacaoRepository localizacaoRepository = new LocalizacaoRepository(ConfigurationManager.ConnectionStrings["conexao"].ToString());
                LocalizacaoAplication localizacaoAplication = new LocalizacaoAplication(localizacaoRepository);

                List<ProjetoSistemasWeb.Domain.Entities.Localizacao> localizacao = localizacaoAplication.Procurar(codigoProduto);

                foreach (var loc in localizacao)
                {
                    localizacaoModel.Add(new Localizacao()
                    {
                        CodigoProduto = loc.CodigoProduto,
                        Data = loc.Data,
                        Lat = loc.Lat,
                        Long = loc.Long
                    });
                }

                return Request.CreateResponse(HttpStatusCode.OK, localizacaoModel);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        public HttpResponseMessage Post([FromBody] Localizacao localizacao)
        {
            try
            {
                //Inclusão do CLiente na base de dados
                //Essa inclusão retorna um Id
                //Id retorna para o requisitante do serviço
                LocalizacaoRepository localizacaoRepository = new LocalizacaoRepository(ConfigurationManager.ConnectionStrings["conexao"].ToString());
                LocalizacaoAplication localizacaoAplication = new LocalizacaoAplication(localizacaoRepository);

                //Converter o model para uma entidade de dominio

                ProjetoSistemasWeb.Domain.Entities.Localizacao localizacaoDomain = new ProjetoSistemasWeb.Domain.Entities.Localizacao()
                {
                    CodigoProduto = localizacao.CodigoProduto,
                    Data = localizacao.Data,
                    Lat = localizacao.Lat,
                    Long = localizacao.Long
                };

                localizacaoAplication.Inserir(localizacaoDomain);

                return Request.CreateResponse(HttpStatusCode.OK, Convert.ToString(localizacaoDomain.CodigoProduto));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
