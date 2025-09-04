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
using ViaCep;

namespace Sistema.Forms
{
    public partial class FrmClientes : Form
    {
        private readonly ClienteDAO dao = new ClienteDAO();

        private bool editar;
        private int id;
        
        public FrmClientes()
        {
            InitializeComponent();

            this.BackColor = ThemeProvider.primary;
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            //LoadTheme();
            Listar();
        }

        private void LoadTheme()
        {
            foreach (Control btns in this.Controls)
            {
                if (btns.GetType() == typeof(Button))
                {
                    Button btn = (Button)btns;
                    btn.BackColor = ThemeColor.PrimaryColor;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
                }
            }
        }

        private void Listar()
        {
            DataTable dt = dao.Listar();

            foreach (DataRow dr in dt.Rows)
            {
                String aux = dr["telefone"].ToString();
                aux = Mask(aux, 0, "(");
                aux = Mask(aux, 3, ") ");
                aux = Mask(aux, 10, "-");
                dr.SetField("telefone", aux);

                aux = dr["cpf"].ToString();
                aux = Mask(aux, 3, ".");
                aux = Mask(aux, 7, ".");
                aux = Mask(aux, 11, "-");
                dr.SetField("cpf", aux);
            }

            grid.DataSource = dt;

            FormatarGrid();
            DesabilitarCampos();
        }

        private string Mask(string text, int startIndex, string mask)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            string result = text;
            int starLength = mask.Length;

            if (text.Length >= startIndex)
            {
                result = text.Insert(startIndex, mask);
            }

            return result;
        }

        private void PreencherCampos()
        {
            txtNome.Text = grid.CurrentRow.Cells[1].Value.ToString();
            mtxtCPF.Text = grid.CurrentRow.Cells[2].Value.ToString();
            mtxtDataNascimento.Text = grid.CurrentRow.Cells[3].Value.ToString();
            mtxtTelefone.Text = grid.CurrentRow.Cells[4].Value.ToString();
            txtEmail.Text = grid.CurrentRow.Cells[5].Value.ToString();
            mtxtCEP.Text = grid.CurrentRow.Cells[6].Value.ToString();
            txtEndereco.Text = grid.CurrentRow.Cells[7].Value.ToString();
            txtBairro.Text = grid.CurrentRow.Cells[8].Value.ToString();
            txtComplemento.Text = grid.CurrentRow.Cells[9].Value.ToString();
            txtCidade.Text = grid.CurrentRow.Cells[10].Value.ToString();
            txtUF.Text = grid.CurrentRow.Cells[11].Value.ToString();
        }

        private void grid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            PreencherCampos();
        }

        private void FormatarGrid()
        {

            grid.Columns[1].HeaderText = "Nome";
            grid.Columns[2].HeaderText = "CPF";
            grid.Columns[3].HeaderText = "Data de nascimento";
            grid.Columns[4].HeaderText = "Telefone";
            grid.Columns[5].HeaderText = "E-mail";
            grid.Columns[6].HeaderText = "CEP";
            grid.Columns[7].HeaderText = "Endereço";
            grid.Columns[8].HeaderText = "Bairro";
            grid.Columns[9].HeaderText = "Complemento";
            grid.Columns[10].HeaderText = "Cidade";
            grid.Columns[11].HeaderText = "UF";

            grid.Columns[0].Visible = false;
            for (int i = 5; i <= 11; i++)
            {
                grid.Columns[i].Visible = false;
            }
        }

        private void DesabilitarCampos()
        {
            txtNome.Enabled = false;
            mtxtCPF.Enabled = false;
            mtxtTelefone.Enabled = false;
            txtEmail.Enabled = false;
            mtxtCEP.Enabled = false;
            txtEndereco.Enabled = false;
            txtBairro.Enabled = false;
            txtComplemento.Enabled = false;
            txtCidade.Enabled = false;
            txtUF.Enabled = false;
            mtxtDataNascimento.Enabled = false;
        }

        private void HabilitarCampos()
        {
            txtNome.Enabled = true;
            mtxtCPF.Enabled = true;
            mtxtTelefone.Enabled = true;
            txtEmail.Enabled = true;
            mtxtCEP.Enabled = true;
            txtEndereco.Enabled = true;
            txtBairro.Enabled = true;
            txtComplemento.Enabled = true;
            txtCidade.Enabled = true;
            txtUF.Enabled = true;
            mtxtDataNascimento.Enabled = true;
        }

        private void LimparCampos()
        {
            txtNome.Clear();
            mtxtCPF.Clear();
            mtxtTelefone.Clear();
            txtEmail.Clear();
            mtxtCEP.Clear();
            txtEndereco.Clear();
            txtBairro.Clear();
            txtComplemento.Clear();
            txtCidade.Clear();
            txtUF.Clear();
            mtxtDataNascimento.Clear();
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            LimparCampos();
            HabilitarCampos();
            txtNome.Focus();
            btnCancelar.Enabled = true;
            btnInserir.Enabled = false;
            btnSalvar.Enabled = true;
            btnExcluir.Enabled = false;
            btnEditar.Enabled = false;
            grid.Enabled = false;
            editar = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DesabilitarCampos();
            btnCancelar.Enabled = false;
            btnInserir.Enabled = true;
            btnSalvar.Enabled = false;
            btnExcluir.Enabled = true;
            btnEditar.Enabled = true;
            grid.Enabled = true;
            PreencherCampos();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            HabilitarCampos();
            btnCancelar.Enabled = true;
            btnInserir.Enabled = false;
            btnSalvar.Enabled = true;
            btnExcluir.Enabled = false;
            btnEditar.Enabled = false;
            grid.Enabled = false;
            editar = true;
            id = Convert.ToInt32(grid.CurrentRow.Cells[0].Value);
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.ToString().Trim() == "")
            {
                FrmMessageBox mb = new FrmMessageBox();
                mb.Show("Preencha o campo Nome", "Cadastro de Clientes");
                txtNome.Text = "";
                txtNome.Focus();
                return;
            }

            Cliente cliente = new Cliente
            {
                id = id,
                nome = txtNome.Text,
                cpf = mtxtCPF.Text,
                data_nascimento = Convert.ToDateTime(value: mtxtDataNascimento.Text),
                telefone = mtxtTelefone.Text,
                email = txtEmail.Text,
                cep = mtxtCEP.Text,
                endereco = txtEndereco.Text,
                bairro = txtBairro.Text,
                complemento = txtComplemento.Text,
                cidade = txtCidade.Text,
                uf = txtUF.Text,
            };

            bool sucesso = false;
            if (!editar)
            {
                if (dao.Cadastrar(cliente))
                    sucesso = true;
            }
            else
            {
                if (dao.Editar(cliente))
                    sucesso = true;
            }

            if (sucesso)
            {
                FrmMessageBox mb = new FrmMessageBox();
                mb.Show("Registro salvo com sucesso!", "Cadastro de Clientes");

                btnCancelar.Enabled = false;
                btnInserir.Enabled = true;
                btnSalvar.Enabled = false;
                btnExcluir.Enabled = true;
                btnEditar.Enabled = true;
                grid.Enabled = true;
                Listar();
                DesabilitarCampos();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            id = Convert.ToInt32(grid.CurrentRow.Cells[0].Value);
            FrmMessageBox mb = new FrmMessageBox();
            mb.Confirm("Deseja realmente excluir este registro?", "Confirmação", true);
            if (mb.DialogResult == DialogResult.Yes)
            {
                try
                {
                    dao.Excluir(id);

                    FrmMessageBox mb2 = new FrmMessageBox();
                    mb2.Show("Registro deletado com sucesso!", "Cadastro de Clientes", true);
                }
                catch (Exception)
                {
                    //
                }
            }

            Listar();
            DesabilitarCampos();
        }

        private void mtxtCEP_Leave(object sender, EventArgs e)
        {
            LocalizarCEP();
        }

        private void LocalizarCEP()
        {
            if (!string.IsNullOrEmpty(mtxtCEP.Text))
            {
                var result = new ViaCepClient().Search(mtxtCEP.Text);
                var address = result.Street;
                var city = result.City;
                var uf = result.StateInitials;
                var district = result.Neighborhood;
                var complement = result.Complement;
                txtEndereco.Text = address.ToString();
                txtBairro.Text = district.ToString();
                txtComplemento.Text = complement.ToString();
                txtCidade.Text = city.ToString();
                txtUF.Text = uf.ToString();
            }
        }

        private void txtBuscaNome_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                DataTable dt = dao.BuscarPorNome(txtBuscaNome.Text);

                foreach (DataRow dr in dt.Rows)
                {
                    String aux = dr["telefone"].ToString();
                    aux = Mask(aux, 0, "(");
                    aux = Mask(aux, 3, ") ");
                    aux = Mask(aux, 10, "-");
                    dr.SetField("telefone", aux);

                    aux = dr["cpf"].ToString();
                    aux = Mask(aux, 3, ".");
                    aux = Mask(aux, 7, ".");
                    aux = Mask(aux, 11, "-");
                    dr.SetField("cpf", aux);
                }

                grid.DataSource = dt;
            }
        }

        private void mtxtBuscaCPF_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                DataTable dt = dao.BuscarListaPorCPF(mtxtBuscaCPF.Text);

                foreach (DataRow dr in dt.Rows)
                {
                    String aux = dr["telefone"].ToString();
                    aux = Mask(aux, 0, "(");
                    aux = Mask(aux, 3, ") ");
                    aux = Mask(aux, 10, "-");
                    dr.SetField("telefone", aux);

                    aux = dr["cpf"].ToString();
                    aux = Mask(aux, 3, ".");
                    aux = Mask(aux, 7, ".");
                    aux = Mask(aux, 11, "-");
                    dr.SetField("cpf", aux);
                }

                grid.DataSource = dt;
            }
        }

        private void mtxtCPF_Leave(object sender, EventArgs e)
        {
            if (!MainClass.ValidarCPF(mtxtCPF.Text))
            {
                lblValidaCPF.Text = "CPF inválido!";
                lblValidaCPF.Visible = true;
            } else
            {
                lblValidaCPF.Visible = false;
            }
        }
    }
}
