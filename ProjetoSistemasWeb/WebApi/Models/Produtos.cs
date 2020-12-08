using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Produtos
    {
        public Guid Id { get; set; }
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public Categorias Categoria { get; set; }
        public int Acessos { get; set; }
        public double Preco { get; set; }
        public Guid IdCategoria { get; set; }
        public string DescCat { get; set; }
    }
}