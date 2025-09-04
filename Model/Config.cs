using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Model
{
    internal class Config
    {
        public int id {  get; set; }
        public bool controle_caixa { get; set; }
        public bool verificar_estoque {  get; set; }
        public bool confirmar_sem_cliente { get; set; }
        public bool notificacao_aniversario { get; set; }
        public bool notificacao_contas_pagar { get; set; }
        public bool notificacao_contas_receber { get; set; }
        public string primary_color { get; set; }
        public string secondary_color { get; set; }
        public string titles_bg_color { get; set; }
    }
}
