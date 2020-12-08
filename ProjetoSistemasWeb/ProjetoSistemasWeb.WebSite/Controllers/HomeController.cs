using Ftec.ProjWeb.Aplicativo1.SiteWeb.Filters;
using ProjetoSistemasWeb.WebSite.API;
using ProjetoSistemasWeb.WebSite.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoSistemasWeb.WebSite.Controllers
{
    [FiltroExcessao]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            APIHttpClient localizacaoHttp = new APIHttpClient("http://pic-buy.brazilsouth.cloudapp.azure.com:81/api/");

            var listaLocalizacao = localizacaoHttp.Get<List<Localizacao>>("localizacao");

            ViewBag.listaLocalizacao = listaLocalizacao;

            return View();
        }
    }
}