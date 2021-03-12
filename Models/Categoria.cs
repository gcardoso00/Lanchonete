using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LanchesMac.Models
{
    public class Categoria
    {
        public int CategoriaID { get; set; }

        [StringLength(100)]
        public string CategoriaNome { get; set; }

        [StringLength(200)]
        public string Descricao { get; set; }
        public List<Lanche> Lanches { get; set; }
    }
}
