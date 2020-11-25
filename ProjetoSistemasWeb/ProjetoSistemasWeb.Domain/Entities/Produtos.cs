﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoSistemasWeb.Domain.Entities
{
    public class Produtos
    {
        public Guid Id { get; set; }
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public Categorias Categoria { get; set; }
        public int Acessos { get; set; }
        public float Preco { get; set; }

        public Produtos()
        {
            this.Categoria = new Categorias();
        }
    }
}
