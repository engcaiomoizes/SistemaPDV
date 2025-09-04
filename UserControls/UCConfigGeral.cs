using Sistema.DAO;
using Sistema.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.UserControls
{
    public partial class UCConfigGeral : UserControl
    {
        private readonly ConfigDAO dao = new ConfigDAO();
        
        public UCConfigGeral()
        {
            InitializeComponent();
        }

        private void UCConfigGeral_Load(object sender, EventArgs e)
        {
            PreencherGeral();
        }

        private void PreencherGeral()
        {
            Config config = dao.ObterGeral();

            if (config.controle_caixa) ckbControleCaixa.Checked = true;
            else ckbControleCaixa.Checked = false;

            if (config.verificar_estoque) ckbVerificarEstoque.Checked = true;
            else ckbVerificarEstoque.Checked = false;

            if (config.confirmar_sem_cliente) ckbConfirmarSemCliente.Checked = true;
            else ckbConfirmarSemCliente.Checked = false;

            if (config.notificacao_aniversario) ckbNotificacaoAniversario.Checked = true;
            else ckbNotificacaoAniversario.Checked = false;

            if (config.notificacao_contas_pagar) ckbNotificacaoContasPagar.Checked = true;
            else ckbNotificacaoContasPagar.Checked = false;

            if (config.notificacao_contas_receber) ckbNotificacoesContasReceber.Checked = true;
            else ckbNotificacoesContasReceber.Checked = false;
        }
    }
}
