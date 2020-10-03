﻿using Ftec.ProjWeb.Aplicativo1.SiteWeb.Filters;
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
            return View();
        }
    }
}