using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Model
{
    internal class Compra
    {
        public int id {  get; set; }
        public DateTime data_cadastro { get; set; }
        public string descricao { get; set; }
        public decimal valor_unitario { get; set; }
        public int quantidade { get; set; }
        public decimal valor_total {  get; set; }
        public int metodo {  get; set; }
        public string observacoes { get; set; }
    }
}
