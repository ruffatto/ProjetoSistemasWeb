using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Localizacao
    {
        public Guid CodigoProduto { get; set; }
        public DateTime Data { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
    }
}