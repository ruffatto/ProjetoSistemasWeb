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
    public class RelatoriosController : Controller
    {
        // GET: Relatorios
        public ActionResult Mapa()
        {
            return View();
        }

        public ActionResult Produtos()
        {
            APIHttpClient produtoHttp = new APIHttpClient("http://pic-buy.brazilsouth.cloudapp.azure.com:81/api/");

            var listaProdutos = produtoHttp.Get<List<Produtos>>("produtos");

            ViewBag.listaProdutos = listaProdutos;

            return View();
        }

        public ActionResult VariacaoDePrecos()
        {
            return View();
        }
    }
}