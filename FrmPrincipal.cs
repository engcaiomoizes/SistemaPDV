using Sistema.DAO;
using Sistema.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema
{
    public partial class FrmPrincipal : Form
    {
        private Button currentButton;
        private ToolStripMenuItem currentMenu;
        private Random random;
        private int tempIndex;
        private Form activeForm;

        private readonly ConfigDAO configDAO = new ConfigDAO();
        private readonly CaixaDAO caixaDAO = new CaixaDAO();

        public FrmPrincipal()
        {
            InitializeComponent();

            random = new Random();
            btnCloseForm.Visible = false;

            menuStrip1.Renderer = new myRenderer();
        }

        static FrmPrincipal _obj;
        public static FrmPrincipal Instance
        {
            get { if (_obj == null) { _obj = new FrmPrincipal(); } return _obj; }
        }

        private class myRenderer : ToolStripProfessionalRenderer
        {
            public myRenderer()
            {
                this.RoundedEdges = true;
            }
            
            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                if (!e.Item.Selected)
                    base.OnRenderMenuItemBackground(e);
                else
                {
                    Rectangle menuRectangle = new Rectangle(Point.Empty, e.Item.Size);
                    
                    e.Graphics.FillRectangle(Brushes.Green, menuRectangle);
                    e.Graphics.DrawRectangle(Pens.DarkGreen, 1, 0, menuRectangle.Width - 2, menuRectangle.Height - 1);
                }
                if (e.Item.Pressed)
                {
                    Rectangle menuRectangle = new Rectangle(Point.Empty, e.Item.Size);

                    e.Graphics.FillRectangle(Brushes.Green, menuRectangle);
                    e.Graphics.DrawRectangle(Pens.DarkGreen, 1, 0, menuRectangle.Width - 2, menuRectangle.Height - 1);
                }
            }
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            _obj = this;
            lblStatusData.Text = DateTime.Today.ToString("dd/MM/yyyy");
            lblStatusHora.Text = DateTime.Now.ToString("HH:mm:ss");
            lblUsuarioLogado.Text = Program.NomeUsuario;
        }

        private Color SelectThemeColor()
        {
            int index = random.Next(ThemeColor.ColorList.Count);
            while (tempIndex == index)
            {
                index = random.Next(ThemeColor.ColorList.Count);
            }
            tempIndex = index;
            string color = ThemeColor.ColorList[index];
            return ColorTranslator.FromHtml(color);
        }

        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (btnSender.GetType() != typeof(Button))
                {
                    if (currentMenu != (ToolStripMenuItem)btnSender)
                    {
                        DisableButton();
                        Color color = SelectThemeColor();
                        currentMenu = (ToolStripMenuItem)btnSender;
                        currentButton = null;
                        pnlTitulo.BackColor = color;
                        pnlLogo.BackColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                        ThemeColor.PrimaryColor = color;
                        ThemeColor.SecondaryColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                        btnCloseForm.Visible = true;
                    }
                } else
                {
                    if (currentButton != (Button)btnSender)
                    {
                        DisableButton();
                        Color color = SelectThemeColor();
                        currentMenu = null;
                        currentButton = (Button)btnSender;
                        currentButton.BackColor = color;
                        currentButton.ForeColor = Color.White;
                        currentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        pnlTitulo.BackColor = color;
                        pnlLogo.BackColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                        ThemeColor.PrimaryColor = color;
                        ThemeColor.SecondaryColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                        btnCloseForm.Visible = true;
                    }
                }
            }
        }

        private void DisableButton()
        {
            foreach (Control previousBtn in pnlMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(51, 51, 76);
                    previousBtn.ForeColor = Color.Gainsboro;
                }
            }
        }

        private void abrirForm(Form childForm, object btnSender)
        {
            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.pnlForms.Controls.Add(childForm);
            this.pnlForms.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void openChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
            {
                if (activeForm.Name.Equals("FrmVendaPDV"))
                {
                    FrmMessageBox mb = new FrmMessageBox();
                    mb.Confirm("Deseja realmente cancelar a venda?", "Cancelar Venda", false);
                    if (mb.DialogResult == DialogResult.Yes)
                    {
                        activeForm.Close();

                        abrirForm(childForm, btnSender);
                    }
                } else
                {
                    activeForm.Close();

                    abrirForm(childForm, btnSender);
                }
            } else
            {
                abrirForm(childForm, btnSender);
            }
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            openChildForm(new Forms.FrmClientes(), sender);
            lblTitulo.Text = "CADASTRO DE CLIENTES";
        }

        private void btnFuncionarios_Click(object sender, EventArgs e)
        {
            openChildForm(new Forms.FrmFuncionarios(), sender);
            lblTitulo.Text = "CADASTRO DE FUNCIONÁRIOS";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
            {
                if (activeForm.Name.Equals("FrmVendaPDV"))
                {
                    FrmMessageBox mb = new FrmMessageBox();
                    mb.Confirm("Deseja realmente cancelar a venda?", "Cancelar Venda", false);
                    if (mb.DialogResult == DialogResult.Yes)
                    {
                        activeForm.Close();
                        Reset();
                    }
                } else
                {
                    activeForm.Close();
                    Reset();
                }
            }
        }

        public void FecharForm()
        {
            if (activeForm != null)
            {
                activeForm.Close();
                activeForm = null;
                Reset();
            }
        }

        private void Reset()
        {
            DisableButton();
            lblTitulo.Text = "Página Inicial";
            pnlTitulo.BackColor = Color.FromArgb(0, 156, 136);
            pnlLogo.BackColor = Color.FromArgb(39, 39, 58);
            currentButton = null;
            currentMenu = null;
            btnCloseForm.Visible = false;
            activeForm = null;
        }

        public void Resetar()
        {
            Reset();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            lblStatusData.Text = DateTime.Today.ToString("dd/MM/yyyy");
            lblStatusHora.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void FrmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnProdutos_Click(object sender, EventArgs e)
        {
            openChildForm(new Forms.FrmProdutos(), sender);
            lblTitulo.Text = "CADASTRO DE PRODUTOS";
        }

        private void btnVendas_Click(object sender, EventArgs e)
        {
            FrmMessageBox mb = new FrmMessageBox();
            mb.Cliente("Iniciando nova venda...", true);
            if (mb.DialogResult == DialogResult.OK)
            {
                FrmVendaPDV frmVendaPDV = new FrmVendaPDV();
                frmVendaPDV.setCPF(mb.cpf);
                openChildForm(frmVendaPDV, sender);
                lblTitulo.Text = "VENDA PDV";
            } else
            {
                //
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            FrmMessageBox mb = new FrmMessageBox();
            mb.Confirm("Deseja realmente sair do sistema?", "Confirmação", true);
            if (mb.DialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void subUsuarios_Click(object sender, EventArgs e)
        {
            openChildForm(new Forms.FrmUsuarios(), sender);
            lblTitulo.Text = "CADASTRO DE USUÁRIOS";
        }

        private void tsbTrocarUsuario_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new Forms.FrmClientes(), sender);
            lblTitulo.Text = "CADASTRO DE CLIENTES";
        }

        private void produtosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new Forms.FrmProdutos(), sender);
            lblTitulo.Text = "CADASTRO DE PRODUTOS";
        }

        private void funcionáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new Forms.FrmFuncionarios(), sender);
            lblTitulo.Text = "CADASTRO DE FUNCIONÁRIOS";
        }

        private void métodosDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new Forms.FrmMetodos(), sender);
            lblTitulo.Text = "MÉTODOS DE PAGAMENTO";
        }

        private void fornecedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new Forms.FrmFornecedores(), sender);
            lblTitulo.Text = "CADASTRO DE FORNECEDORES";
        }

        private void cargosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new Forms.FrmCargos(), sender);
            lblTitulo.Text = "CADASTRO DE CARGOS";
        }

        private void vendasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            openChildForm(new Forms.FrmVendas(), sender);
            lblTitulo.Text = "VENDAS EFETUADAS";
        }

        private void btnControleDeCaixa_Click(object sender, EventArgs e)
        {
            bool controleCaixa = true;
            if (!configDAO.ControleDeCaixa())
            {
                controleCaixa = false;
                FrmMessageBox mb = new FrmMessageBox();
                mb.Confirm("O Controle de Caixa está desativado. Deseja ativá-lo?", "Controle de Caixa", true);
                if (mb.DialogResult == DialogResult.Yes)
                {
                    configDAO.AtivarControleCaixa();
                    controleCaixa = true;
                }
            }
            
            if (controleCaixa)
            {
                if (!caixaDAO.ExisteCaixaAberto())
                {
                    FrmMessageBox mb = new FrmMessageBox();
                    mb.Confirm("Não existe nenhum caixa aberto. Deseja abrir um novo caixa?", "Controle de Caixa", true);
                    if (mb.DialogResult == DialogResult.Yes)
                    {
                        FrmAbrirCaixa abrirCaixa = new FrmAbrirCaixa();
                        MainClass.BlurBackground(abrirCaixa);
                        if (abrirCaixa.DialogResult == DialogResult.OK)
                        {
                            FrmMessageBox mb2 = new FrmMessageBox();
                            mb2.Show("Caixa aberto com sucesso!", "Abertura de Caixa", true);
                        }
                    }
                }
                else
                {
                    openChildForm(new Forms.FrmControleDeCaixa(), sender);
                    lblTitulo.Text = "CONTROLE DE CAIXA";
                }
            }
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            FrmConfiguracoes config = new FrmConfiguracoes();
            MainClass.BlurBackground(config);
        }

        private void controlarEstoqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new Forms.FrmEstoque(), sender);
            lblTitulo.Text = "CONTROLE DE ESTOQUE";
        }

        private void fluxoDeCaixaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool controleCaixa = true;
            if (!configDAO.ControleDeCaixa())
            {
                controleCaixa = false;
                FrmMessageBox mb = new FrmMessageBox();
                mb.Confirm("O Controle de Caixa está desativado. Deseja ativá-lo?", "Controle de Caixa", true);
                if (mb.DialogResult == DialogResult.Yes)
                {
                    configDAO.AtivarControleCaixa();
                    controleCaixa = true;
                }
            }

            if (controleCaixa)
            {
                if (!caixaDAO.ExisteCaixaAberto())
                {
                    FrmMessageBox mb = new FrmMessageBox();
                    mb.Confirm("Não existe nenhum caixa aberto. Deseja abrir um novo caixa?", "Controle de Caixa", true);
                    if (mb.DialogResult == DialogResult.Yes)
                    {
                        FrmAbrirCaixa abrirCaixa = new FrmAbrirCaixa();
                        MainClass.BlurBackground(abrirCaixa);
                        if (abrirCaixa.DialogResult == DialogResult.OK)
                        {
                            FrmMessageBox mb2 = new FrmMessageBox();
                            mb2.Show("Caixa aberto com sucesso!", "Abertura de Caixa", true);
                        }
                    }
                }
                else
                {
                    openChildForm(new Forms.FrmControleDeCaixa(), sender);
                    lblTitulo.Text = "CONTROLE DE CAIXA";
                }
            }
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSobre frmSobre = new FrmSobre();
            MainClass.BlurBackground(frmSobre);
        }
    }
}
