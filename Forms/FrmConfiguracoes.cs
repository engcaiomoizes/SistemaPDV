using Sistema.DAO;
using Sistema.Model;
using Sistema.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.Forms
{   
    public partial class FrmConfiguracoes : Form
    {
        private readonly ConfigDAO dao = new ConfigDAO();
        private UCConfigGeral geral;
        private UCConfigDados dados;
        private UCConfigTema tema;

        public FrmConfiguracoes()
        {
            InitializeComponent();
        }

        private void FrmConfiguracoes_Load(object sender, EventArgs e)
        {
            //foreach (Panel panel in pnlMenu.Controls)
            //{
            //    panel.Paint += limparPintura;
            //}
            //pnlBtnGeral.Paint += pintarBotoes;

            geral = new UCConfigGeral();
            dados = new UCConfigDados();
            tema = new UCConfigTema();

            addUserControl(geral);
        }

        private void pintarBotoes(object sender, PaintEventArgs e)
        {
            Panel btn = (Panel)sender;
            if (btn.BorderStyle == BorderStyle.None)
            {
                int thickness = 2;
                int halfThickness = thickness / 2;
                using (Pen p = new Pen(Color.White, thickness))
                {
                    e.Graphics.DrawLine(p, 0, btn.Height - halfThickness, btn.Width, btn.Height - halfThickness);
                }
            }
        }

        private void limparPintura(object sender, PaintEventArgs e)
        {
            Panel btn = (Panel)sender;
            if (btn.BorderStyle == BorderStyle.None)
            {
                int thickness = 2;
                int halfThickness = thickness / 2;
                using (Pen p = new Pen(Color.FromArgb(211, 84, 0), thickness))
                {
                    e.Graphics.DrawLine(p, 0, btn.Height - halfThickness, btn.Width, btn.Height - halfThickness);
                }
            }
        }

        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            pnlContainer.Controls.Clear();
            pnlContainer.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void btnGeral_Click(object sender, EventArgs e)
        {
            //foreach (Panel panel in pnlMenu.Controls)
            //{
            //    panel.Paint += limparPintura;
            //}
            //pnlBtnGeral.Paint += pintarBotoes;

            addUserControl(geral);
        }

        private void btnDados_Click(object sender, EventArgs e)
        {
            //foreach (Panel panel in pnlMenu.Controls)
            //{
            //    panel.Paint += limparPintura;
            //}
            //pnlBtnDados.Paint += pintarBotoes;

            addUserControl(dados);
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Loja loja = new Loja
            {
                nome_razao = dados.txtRazao.Text,
                nome_fantasia = dados.txtFantasia.Text,
                cnpj = dados.mtxtCNPJ.Text,
                telefone = dados.mtxtTelefone.Text,
                email = dados.txtEmail.Text,
                cep = dados.mtxtCEP.Text,
                endereco = dados.txtEndereco.Text,
                bairro = dados.txtBairro.Text,
                complemento = dados.txtComplemento.Text,
                cidade = dados.txtCidade.Text,
                uf = dados.txtUF.Text,
            };
            Config config = new Config
            {
                controle_caixa = geral.ckbControleCaixa.Checked,
                verificar_estoque = geral.ckbVerificarEstoque.Checked,
                confirmar_sem_cliente = geral.ckbConfirmarSemCliente.Checked,
                notificacao_aniversario = geral.ckbNotificacaoAniversario.Checked,
                notificacao_contas_pagar = geral.ckbNotificacaoContasPagar.Checked,
                notificacao_contas_receber = geral.ckbNotificacoesContasReceber.Checked,
                primary_color = tema.GetPrimaryColor(),
                secondary_color = tema.GetSecondaryColor(),
                titles_bg_color = tema.GetTitlesBgColor(),
            };
            if ((dao.AtualizarDados(loja)) && (dao.AtualizarGeral(config)))
            {
                FrmMessageBox mb = new FrmMessageBox();
                mb.Show("Informações atualizadas com sucesso!", "Configurações", true);
                DialogResult = DialogResult.OK;
            }
        }

        private void btnTema_Click(object sender, EventArgs e)
        {
            addUserControl(tema);
        }
    }
}
