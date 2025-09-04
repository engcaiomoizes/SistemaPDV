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
    public partial class FrmProdutos : Form
    {
        private readonly ProdutoDAO dao = new ProdutoDAO();
        private readonly FornecedorDAO fornecedorDAO = new FornecedorDAO();

        private bool editar;
        private int id;

        public FrmProdutos()
        {
            InitializeComponent();
        }

        private void FrmProdutos_Load(object sender, EventArgs e)
        {
            Listar();
            cbbFiltro.SelectedIndex = 0;
            grid.Focus();
        }

        private void Listar()
        {
            DataTable dt = dao.Listar();

            grid.DataSource = dt;

            cbbFornecedor.DataSource = fornecedorDAO.Listar();
            cbbFornecedor.DisplayMember = "nome";
            cbbFornecedor.ValueMember = "id";

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

            grid.Columns[1].HeaderText = "Cód. Barras";
            grid.Columns[2].HeaderText = "Descrição";
            grid.Columns[4].HeaderText = "Fornecedor";
            grid.Columns[6].HeaderText = "Estoque";
            grid.Columns[7].HeaderText = "Preço Custo";
            grid.Columns[7].ValueType = typeof(string);
            grid.Columns[8].HeaderText = "Preço Venda";
            grid.Columns[8].ValueType = typeof(string);

            grid.Columns[7].DefaultCellStyle.Format = "c";
            grid.Columns[8].DefaultCellStyle.Format = "c";

            grid.Columns[0].Visible = false;
            //grid.Columns[1].Visible = false;
            grid.Columns[3].Visible = false;
            grid.Columns[4].Visible = false;
            grid.Columns[5].Visible = false;
            grid.Columns[9].Visible = false;
            grid.Columns[10].Visible = false;
            grid.Columns[11].Visible = false;
            grid.Columns[12].Visible = false;
            grid.Columns[13].Visible = false;
            grid.Columns[14].Visible = false;
        }

        private void DesabilitarCampos()
        {
            txtBarcode.Enabled = false;
            txtDescricao.Enabled = false;
            txtEmbalagem.Enabled = false;
            cbbFornecedor.Enabled = false;
            txtEstoqueMin.Enabled = false;
            txtEstoque.Enabled = false;
            txtCusto.Enabled = false;
            txtValor.Enabled = false;
            ckbAtivo.Enabled = false;
            ckbControlarEstoque.Enabled = false;
        }

        private void HabilitarCampos()
        {
            txtBarcode.Enabled = true;
            txtDescricao.Enabled = true;
            txtEmbalagem.Enabled = true;
            cbbFornecedor.Enabled = true;
            txtEstoqueMin.Enabled = true;
            txtEstoque.Enabled = true;
            txtCusto.Enabled = true;
            txtValor.Enabled = true;
            ckbAtivo.Enabled = true;
            ckbControlarEstoque.Enabled = true;
        }

        private void LimparCampos()
        {
            txtBarcode.Clear();
            txtDescricao.Clear();
            txtEmbalagem.Clear();
            txtEstoqueMin.Clear() ;
            txtEstoque.Clear();
            txtCusto.Clear();
            txtValor.Clear();
            ckbAtivo.Checked = true;
            ckbControlarEstoque.Checked = false;
        }

        private void PreencherCampos()
        {
            if (grid.Rows.Count > 0)
            {
                txtBarcode.Text = grid.CurrentRow.Cells[1].Value.ToString();
                txtDescricao.Text = grid.CurrentRow.Cells[2].Value.ToString();
                txtEmbalagem.Text = grid.CurrentRow.Cells[3].Value.ToString();
                cbbFornecedor.SelectedValue = grid.CurrentRow.Cells[11].Value;
                txtEstoqueMin.Text = grid.CurrentRow.Cells[5].Value.ToString();
                txtEstoque.Text = grid.CurrentRow.Cells[6].Value.ToString();
                txtCusto.Text = grid.CurrentRow.Cells[7].Value.ToString();
                txtValor.Text = grid.CurrentRow.Cells[8].Value.ToString();
                if (MainClass.ConvertCharToBool(Convert.ToChar(grid.CurrentRow.Cells[10].Value)))
                {
                    ckbAtivo.Checked = true;
                }
                else
                {
                    ckbAtivo.Checked = false;
                }
                if (MainClass.ConvertCharToBool(Convert.ToChar(grid.CurrentRow.Cells[14].Value)))
                    ckbControlarEstoque.Checked = false;
                else
                    ckbControlarEstoque.Checked = true;
            } else
            {
                txtBarcode.Clear();
                txtDescricao.Clear();
                txtEmbalagem.Clear();
                //cbbFornecedor.SelectedIndex = 0;
                txtEstoqueMin.Clear();
                txtEstoque.Clear();
                txtCusto.Clear();
                txtValor.Clear();
                ckbAtivo.Checked = true;
                ckbControlarEstoque.Checked = false;
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
            txtBarcode.Focus();
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

                    mb.Show("Registro deletado com sucesso!", "Cadastro de Produtos");
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
            if (string.IsNullOrEmpty(txtDescricao.Text) || string.IsNullOrEmpty(txtBarcode.Text) || string.IsNullOrEmpty(txtEmbalagem.Text) ||
                string.IsNullOrEmpty(txtEstoqueMin.Text) || string.IsNullOrEmpty(txtEstoque.Text) || string.IsNullOrEmpty(txtCusto.Text) ||
                string.IsNullOrEmpty(txtValor.Text) || string.IsNullOrEmpty(cbbFornecedor.Text))
            {
                FrmMessageBox mb = new FrmMessageBox();
                mb.Show("Preencha todos os campos", "Cadastro de Produtos");
                //txtDescricao.Text = "";
                //txtDescricao.Focus();
                return;
            }

            Produto produto = new Produto
            {
                id = id,
                barcode = txtBarcode.Text,
                descricao = txtDescricao.Text,
                embalagem = txtEmbalagem.Text,
                fornecedor = Convert.ToInt32(cbbFornecedor.SelectedValue),
                estoque_min = Convert.ToUInt16(txtEstoqueMin.Text),
                estoque = Convert.ToUInt16(txtEstoque.Text),
                controlar_estoque = !ckbControlarEstoque.Checked,
                custo = Convert.ToDecimal(txtCusto.Text),
                valor = Convert.ToDecimal(txtValor.Text),
                data_cadastro = DateTime.Now,
                ativo = ckbAtivo.Checked,
            };

            bool sucesso = false;
            if (!editar)
            {
                MessageBox.Show("Entrouu");
                if (dao.Cadastrar(produto))
                    sucesso = true;
            }
            else
            {
                if (dao.Editar(produto))
                    sucesso = true;
            }

            if (sucesso)
            {
                FrmMessageBox mb = new FrmMessageBox();
                mb.Show("Registro salvo com sucesso!", "Cadastro de Produtos");

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
    }
}
