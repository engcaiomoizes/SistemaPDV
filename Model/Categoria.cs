using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Model
{
    internal class Categoria
    {
        public int id {  get; set; }
        public string descricao { get; set; }
        public DateTime data_cadastro { get; set; }
        public bool ativo { get; set; }
    }
}
