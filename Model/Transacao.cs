using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Model
{
    internal class Transacao
    {
        public int id {  get; set; }
        public int caixa { get; set; }
        public decimal valor { get; set; }
        public int? metodo { get; set; }
        public bool entrada { get; set; }
        public string descricao { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
