using Ftec.ProjWeb.Aplicativo1.SiteWeb.Filters;
using ProjetoSistemasWeb.WebSite.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoSistemasWeb.WebSite.Controllers
{
    [FiltroExcessao]
    public class CadastrosController : Controller
    {
        // GET: Cadastros
        public ActionResult Produtos()
        {
            List<Produtos> produtos = (List<Produtos>)Session["produtos"];

            ViewBag.listaProdutos = produtos;

            return View();
        }

        public ActionResult NovoProduto()
        {
            return View("Novo_ajax");
        }

        public ActionResult Categorias()
        {
            return View();
        }
    }
}