using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoSistemasWeb.WebSite.Controllers
{
    public class RelatoriosController : Controller
    {
        // GET: Relatorios
        public ActionResult Mapa()
        {
            return View();
        }

        public ActionResult Valores()
        {
            return View();
        }

        public ActionResult VariacaoDePrecos()
        {
            return View();
        }
    }
}