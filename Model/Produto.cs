using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Model
{
    internal class Produto
    {
        public int id { get; set; }
        public string barcode { get; set; }
        public string descricao { get; set; }
        public string embalagem { get; set; }
        public int fornecedor { get; set; }
        public UInt16 estoque_min {  get; set; }
        public UInt16 estoque {  get; set; }
        public bool controlar_estoque { get; set; }
        public decimal custo { get; set; }
        public decimal valor { get; set; }
        public DateTime data_cadastro { get; set; }
        public bool ativo { get; set; }
    }
}
