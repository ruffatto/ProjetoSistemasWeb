using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoSistemasWeb.WebSite.models
{
    public class Produtos
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public float Preco { get; set; }
        public string Categoria { get; set; }
        public int acessos { get; set; }
    }
}