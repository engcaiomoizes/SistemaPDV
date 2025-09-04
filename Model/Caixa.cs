using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Model
{
    internal class Caixa
    {
        public int id {  get; set; }
        public DateTime data_abertura { get; set; }
        public DateTime? data_fechamento { get; set; }
        public bool status {  get; set; }
        public string apelido { get; set; }
        public DateTime? ultima_atualizacao { get; set; }

        public List<Transacao> transacoes { get; set; }

        public string GetString()
        {
            return "Id: " + id + "\nData Abertura: " + data_abertura + "\nData Fechamento: " + data_fechamento + "\nStatus: " + status + "\nApelido: " + apelido + "\nÚltima atualização: " + ultima_atualizacao;
        }
    }
}
