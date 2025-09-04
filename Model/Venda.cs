using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Model
{
    internal class Venda
    {
        public int id {  get; set; }
        public int? cliente {  get; set; }
        public int funcionario { get; set; }
        public DateTime data_cadastro { get; set; }
        public decimal subtotal { get; set; }
        public decimal desconto { get; set; }
        public decimal acrescimo { get; set; }
        public decimal total { get; set; }
        public int metodo { get; set; }
        public string observacoes { get; set; }

        public Dictionary<int, int> produtos { get; set; }

        public string GetString()
        {
            return "Id: " + id + "\nCliente: " + cliente + "\nFuncionario: " + funcionario + "\nData: " + data_cadastro + "\nSubtotal: " + subtotal + "\nDesconto: " + desconto + "\nAcréscimo: " + acrescimo + "\nTotal: " + total + "\nMétodo: " + metodo + "\nObservações: " + observacoes;
        }
    }
}
