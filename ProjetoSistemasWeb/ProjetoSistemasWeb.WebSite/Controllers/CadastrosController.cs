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
    public class CadastrosController : Controller
    {
        // GET: Cadastros
        public ActionResult Produtos()
        {
            APIHttpClient produtoHttp = new APIHttpClient("http://pic-buy.brazilsouth.cloudapp.azure.com:81/api/");

            var listaProdutos = produtoHttp.Get<List<Produtos>>("produtos");

            ViewBag.listaProdutos = listaProdutos;

            return View();
        }

        public ActionResult NovoProduto()
        {
            APIHttpClient categoriaHttp = new APIHttpClient("http://pic-buy.brazilsouth.cloudapp.azure.com:81/api/");

            var listaCategoria = categoriaHttp.Get<List<Categorias>>("categorias");

            ViewBag.listaCategoria = listaCategoria;
            return View();
        }

        public ActionResult GravarProduto(Produtos produtos)
        {
            Produtos prod = new Produtos()
            {
                Codigo = produtos.Codigo,
                Descricao = produtos.Descricao,
                Imagem = produtos.Imagem,
                Acessos = produtos.Acessos,
                Preco = produtos.Preco,
                IdCategoria = produtos.IdCategoria
            };

            APIHttpClient produtoHttp = new APIHttpClient("http://pic-buy.brazilsouth.cloudapp.azure.com:81/api/");
            var retorno = produtoHttp.Post<Produtos>("produtos",prod);

            return RedirectToAction("Produtos");
        }

        public ActionResult Categorias()
        {
            APIHttpClient categoriaHttp = new APIHttpClient("http://pic-buy.brazilsouth.cloudapp.azure.com:81/api/");

            var listaCategoria = categoriaHttp.Get<List<Categorias>>("categorias");

            ViewBag.listaCategoria = listaCategoria;

            return View();
        }

        public ActionResult NovaCategoria()
        {
            return View();
        }

        public ActionResult GravarCategoria(Categorias categorias)
        {
            Categorias cat = new Categorias()
            {
                Descricao = categorias.Descricao
            };

            APIHttpClient categoriaHttp = new APIHttpClient("http://pic-buy.brazilsouth.cloudapp.azure.com:81/api/");
            var retorno = categoriaHttp.Post<Categorias>("categorias", cat);

            return RedirectToAction("Categorias");
        }
    }
}