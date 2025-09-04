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

namespace Sistema.Forms
{
    public partial class FrmUsuarios : Form
    {
        private readonly UsuarioDAO dao = new UsuarioDAO();
        private readonly FuncionarioDAO funcionarioDAO = new FuncionarioDAO();

        private bool editar;
        private int id;

        public FrmUsuarios()
        {
            InitializeComponent();
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            Listar();
            cbbFiltro.SelectedIndex = 0;
            txtBuscaLogin.Focus();
        }

        private void Listar()
        {
            DataTable dt = dao.ListarUsuarios();

            grid.DataSource = dt;

            cbbFuncionario.DataSource = funcionarioDAO.Listar();
            cbbFuncionario.DisplayMember = "nome";
            cbbFuncionario.ValueMember = "id";

            FormatarGrid();
            DesabilitarCampos();
        }

        private void FormatarGrid()
        {

            grid.Columns[1].HeaderText = "Usuário";
            grid.Columns[2].HeaderText = "Senha";
            grid.Columns[3].HeaderText = "Nome";
            grid.Columns[4].HeaderText = "Ativo?";
            grid.Columns[0].Visible = false;
            grid.Columns[2].Visible = false;
            grid.Columns[5].Visible = false;
        }

        private void DesabilitarCampos()
        {
            txtUsuario.Enabled = false;
            txtSenha.Enabled = false;
            cbbFuncionario.Enabled = false;
            ckbAtivo.Enabled = false;
        }

        private void HabilitarCampos()
        {
            txtUsuario.Enabled = true;
            txtSenha.Enabled = true;
            cbbFuncionario.Enabled = true;
            ckbAtivo.Enabled = true;
        }

        private void LimparCampos()
        {
            txtUsuario.Clear();
            txtSenha.Clear();
            //cbbFuncionario.Items.Clear();
            ckbAtivo.Checked = true;
        }

        private void PreencherCampos()
        {
            txtUsuario.Text = grid.CurrentRow.Cells[1].Value.ToString();
            txtSenha.Text = grid.CurrentRow.Cells[2].Value.ToString();
            cbbFuncionario.SelectedValue = grid.CurrentRow.Cells[5].Value;
            if (MainClass.ConvertCharToBool(Convert.ToChar(grid.CurrentRow.Cells[4].Value)))
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
            txtUsuario.Focus();
            btnCancelar.Enabled = true;
            btnInserir.Enabled = false;
            btnSalvar.Enabled = true;
            btnExcluir.Enabled = false;
            btnEditar.Enabled = false;
            grid.Enabled = false;
            editar = false;
            
            txtBuscaLogin.Enabled = false;
            txtBuscaNome.Enabled = false;
            cbbFiltro.Enabled = false;
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

            txtBuscaLogin.Enabled = true;
            txtBuscaNome.Enabled = true;
            cbbFiltro.Enabled = true;
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
            if (txtUsuario.Text.ToString().Trim() == "" || txtSenha.Text.ToString().Trim() == "" || cbbFuncionario.Text.ToString().Trim() == "")
            {
                FrmMessageBox mb = new FrmMessageBox();
                mb.Show("Preencha todos os campos", "Cadastro de Usuários");
                //MessageBox.Show("Preencha todos os campos", "Cadastro de Usuários");
                //txtNome.Text = "";
                //txtNome.Focus();
                return;
            }

            Usuario usuario = new Usuario
            {
                id = id,
                login = txtUsuario.Text,
                senha = txtSenha.Text,
                funcionario = Convert.ToInt32(cbbFuncionario.SelectedValue),
                ativo = ckbAtivo.Checked
            };

            try
            {
                if (!editar)
                {
                    dao.CadastrarUsuario(usuario);
                } else
                {
                    dao.EditarUsuario(usuario);
                }

                FrmMessageBox mb = new FrmMessageBox();
                mb.Show("Registro salvo com sucesso!", "Cadastro de Usuários");
                //MessageBox.Show("Registro salvo com sucesso!", "Cadastro de Usuários");
            } catch (Exception)
            {
                //
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

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            id = Convert.ToInt32(grid.CurrentRow.Cells[0].Value);
            FrmMessageBox mb = new FrmMessageBox();
            mb.Confirm("Deseja realmente excluir este registro?", "Confirmação", false);
            if (mb.DialogResult == DialogResult.Yes)
            {
                try
                {
                    dao.ExcluirUsuario(id);

                    mb.Show("Registro deletado com sucesso!", "Cadastro de Usuários");
                    //MessageBox.Show("Registro deletado com sucesso!", "Cadastro de Usuários");
                }
                catch (Exception)
                {
                    //
                }
            }

            Listar();
            DesabilitarCampos();
        }

        private void txtBuscaLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                grid.DataSource = dao.BuscarPorLogin(txtBuscaLogin.Text);
            }
        }
    }
}
