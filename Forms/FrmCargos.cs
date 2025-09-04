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
    public partial class FrmCargos : Form
    {
        private readonly CargoDAO dao = new CargoDAO();

        private bool editar;
        private int id;
        
        public FrmCargos()
        {
            InitializeComponent();
        }

        private void Cargos_Load(object sender, EventArgs e)
        {
            Listar();
            cbbFiltro.SelectedIndex = 0;
            grid.Focus();
        }

        private void Listar()
        {
            DataTable dt = dao.Listar();

            grid.DataSource = dt;

            FormatarGrid();
            DesabilitarCampos();
        }

        private void FormatarGrid()
        {
            grid.Columns[1].HeaderText = "Cargo";
            grid.Columns[0].Visible = false;
        }

        private void DesabilitarCampos()
        {
            txtNome.Enabled = false;
        }

        private void HabilitarCampos()
        {
            txtNome.Enabled = true;
        }

        private void LimparCampos()
        {
            txtNome.Clear();
        }

        private void PreencherCampos()
        {
            txtNome.Text = grid.CurrentRow.Cells[1].Value.ToString();
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

            txtBuscaNome.Enabled = false;
            cbbFiltro.Enabled = false;
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

                    mb.Show("Registro deletado com sucesso!", "Cadastro de Cargos");
                }
                catch (Exception)
                {
                    //
                }
            }

            Listar();
            DesabilitarCampos();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.ToString().Trim() == "")
            {
                FrmMessageBox mb = new FrmMessageBox();
                mb.Show("Preencha o Nome do Cargo", "Cadastro de Cargos");
                //txtNome.Text = "";
                //txtNome.Focus();
                return;
            }

            Cargo cargo = new Cargo
            {
                id = id,
                nome = txtNome.Text,
            };

            try
            {
                if (!editar)
                {
                    dao.Cadastrar(cargo);
                }
                else
                {
                    dao.Editar(cargo);
                }

                FrmMessageBox mb = new FrmMessageBox();
                mb.Show("Registro salvo com sucesso!", "Cadastro de Cargos");
            }
            catch (Exception)
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

            txtBuscaNome.Enabled = true;
            cbbFiltro.Enabled = true;
        }

        private void txtBuscaNome_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                grid.DataSource = dao.BuscarPorNome(txtBuscaNome.Text);
            }
        }
    }
}
