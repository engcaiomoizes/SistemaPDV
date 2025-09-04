using Sistema.DAO;
using Sistema.Model;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ViaCep;

namespace Sistema.Forms
{
    public partial class FrmFuncionarios : Form
    {
        
        private readonly FuncionarioDAO dao = new FuncionarioDAO();
        private readonly CargoDAO cargoDAO = new CargoDAO();

        private bool editar;
        private int id;

        public FrmFuncionarios()
        {
            InitializeComponent();
        }

        private void FrmFuncionarios_Load(object sender, EventArgs e)
        {
            Listar();
            cbbFiltro.SelectedIndex = 0;
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

            cbbCargo.DataSource = cargoDAO.Listar();
            cbbCargo.DisplayMember = "nome";
            cbbCargo.ValueMember = "id";

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

        private void FormatarGrid()
        {

            grid.Columns[1].HeaderText = "Nome";
            grid.Columns[2].HeaderText = "CPF";
            grid.Columns[3].HeaderText = "Telefone";
            grid.Columns[4].HeaderText = "E-mail";
            grid.Columns[5].HeaderText = "CEP";
            grid.Columns[6].HeaderText = "Endereço";
            grid.Columns[7].HeaderText = "Bairro";
            grid.Columns[8].HeaderText = "Complemento";
            grid.Columns[9].HeaderText = "Cidade";
            grid.Columns[10].HeaderText = "UF";
            grid.Columns[13].HeaderText = "Ativo?";
            grid.Columns[14].HeaderText = "Cargo";
            grid.Columns[0].Visible = false;
            for (int i = 5; i <= 13; i++)
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
            mtxtDataAdmissao.Enabled = false;
            ckbAtivo.Enabled = false;
            cbbCargo.Enabled = false;
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
            mtxtDataAdmissao.Enabled = true;
            ckbAtivo.Enabled = true;
            cbbCargo.Enabled = true;
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
            mtxtDataAdmissao.Clear();
            ckbAtivo.Checked = true;
        }

        private void PreencherCampos()
        {
            txtNome.Text = grid.CurrentRow.Cells[1].Value.ToString();
            mtxtCPF.Text = grid.CurrentRow.Cells[2].Value.ToString();
            mtxtTelefone.Text = grid.CurrentRow.Cells[3].Value.ToString();
            txtEmail.Text = grid.CurrentRow.Cells[4].Value.ToString();
            mtxtCEP.Text = grid.CurrentRow.Cells[5].Value.ToString();
            txtEndereco.Text = grid.CurrentRow.Cells[6].Value.ToString();
            txtBairro.Text = grid.CurrentRow.Cells[7].Value.ToString();
            txtComplemento.Text = grid.CurrentRow.Cells[8].Value.ToString();
            txtCidade.Text = grid.CurrentRow.Cells[9].Value.ToString();
            txtUF.Text = grid.CurrentRow.Cells[10].Value.ToString();
            mtxtDataAdmissao.Text = grid.CurrentRow.Cells[11].Value.ToString();
            cbbCargo.SelectedValue = grid.CurrentRow.Cells[12].Value;
            if (MainClass.ConvertCharToBool(Convert.ToChar(grid.CurrentRow.Cells[13].Value)))
            {
                ckbAtivo.Checked = true;
            }
            else
            {
                ckbAtivo.Checked = false;
            }
        }

        private void grid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            PreencherCampos();
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

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            id = Convert.ToInt32(grid.CurrentRow.Cells[0].Value);
            FrmMessageBox mb = new FrmMessageBox();
            mb.Confirm("Deseja realmente excluir este registro?", "Confirmação", false);
            if (mb.DialogResult == DialogResult.Yes)
            {
                try
                {
                    dao.Excluir(id);

                    mb.Show("Registro deletado com sucesso!", "Cadastro de Funcionários");
                }
                catch (Exception)
                {
                    //
                }
            }

            Listar();
            DesabilitarCampos();
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
                mb.Show("Preencha o campo Nome", "Cadastro de Funcionários");
                txtNome.Text = "";
                txtNome.Focus();
                return;
            }

            Funcionario funcionario = new Funcionario
            {
                id = id,
                nome = txtNome.Text,
                cpf = mtxtCPF.Text,
                telefone = mtxtTelefone.Text,
                email = txtEmail.Text,
                cep = mtxtCEP.Text,
                endereco = txtEndereco.Text,
                bairro = txtBairro.Text,
                complemento = txtComplemento.Text,
                cidade = txtCidade.Text,
                uf = txtUF.Text,
                cargo = Convert.ToInt32(cbbCargo.SelectedValue),
                ativo = ckbAtivo.Checked,
            };

            if (mtxtDataAdmissao.Text == "  /  /    " || mtxtDataAdmissao.Text.Length < 10)
            {
                funcionario.data_admissao = null;
            } else
            {
                funcionario.data_admissao = mtxtDataAdmissao.Text;
            }

            try
            {
                if (!editar)
                {
                    dao.Cadastrar(funcionario);
                }
                else
                {
                    dao.Editar(funcionario);
                }

                FrmMessageBox mb = new FrmMessageBox();
                mb.Show("Registro salvo com sucesso!", "Cadastro de Funcionários");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            btnCancelar.Enabled = false;
            btnInserir.Enabled = true;
            btnSalvar.Enabled = false;
            btnExcluir.Enabled = true;
            btnEditar.Enabled = true;
            grid.Enabled = true;
            Listar();
            DesabilitarCampos();
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

        private void mtxtCEP_Leave(object sender, EventArgs e)
        {
            LocalizarCEP();
        }

        private void LocalizarCEP()
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
}
