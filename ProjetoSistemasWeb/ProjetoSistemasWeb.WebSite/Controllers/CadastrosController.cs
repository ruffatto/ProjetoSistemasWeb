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
            //List<Produtos> produtos = (List<Produtos>)Session["produtos"];
            //ViewBag.listaProdutos = produtos;

            APIHttpClient produtoHttp = new APIHttpClient("http://localhost:65170/api/");

            var listaProdutos = produtoHttp.Get<List<Produtos>>("produtos");

            ViewBag.listaProdutos = listaProdutos;

            return View();
        }

        public ActionResult GravarProduto()
        {
            Produtos prod = new Produtos()
            {
                Codigo = 4,
                Descricao = "Produto 4",
                Imagem = "gs://leitura-9ce08.appspot.com/Imagens/Busca de Imagem.png",
                Id = Guid.NewGuid(),
                Acessos = 600,
                Preco = 12000,
                Categoria = new Categorias()
                {
                    Descricao = "Cama",
                    Id = Guid.NewGuid()
                }
            };

            APIHttpClient produtoHttp = new APIHttpClient("http://localhost:65170/api/");
            var retorno = produtoHttp.Post<Produtos>("produtos",prod);
            ViewBag.Retorno = retorno.Message;

            return View();
        }

        public ActionResult NovoProduto()
        {
            return View("Novo_ajax");
        }

        public ActionResult Categorias()
        {
            APIHttpClient categoriaHttp = new APIHttpClient("http://localhost:65170/api/");

            var listaCategoria = categoriaHttp.Get<List<Produtos>>("categorias");

            ViewBag.listaCategoria = listaCategoria;

            return View();
        }
    }
}