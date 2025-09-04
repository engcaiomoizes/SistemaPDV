using Sistema.DAO;
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
    public partial class FrmVendas : Form
    {
        private readonly VendaDAO dao = new VendaDAO();

        private int id;

        public FrmVendas()
        {
            InitializeComponent();
        }

        private void FrmVendas_Load(object sender, EventArgs e)
        {
            Listar();
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

            grid.Columns[0].HeaderText = "Cliente";
            grid.Columns[4].HeaderText = "Data da venda";
            grid.Columns[5].HeaderText = "Subtotal";
            grid.Columns[6].HeaderText = "Desconto";
            grid.Columns[7].HeaderText = "Acréscimo";
            grid.Columns[8].HeaderText = "Total";
            grid.Columns[11].HeaderText = "Método de pagamento";

            grid.Columns[5].DefaultCellStyle.Format = "c";
            grid.Columns[6].DefaultCellStyle.Format = "c";
            grid.Columns[7].DefaultCellStyle.Format = "c";
            grid.Columns[8].DefaultCellStyle.Format = "c";

            grid.Columns[1].Visible = false;
            grid.Columns[2].Visible = false;
            grid.Columns[3].Visible = false;
            grid.Columns[9].Visible = false;
            grid.Columns[10].Visible = false;
            grid.Columns[12].Visible = false;
        }

        private void DesabilitarCampos()
        {
            //
        }

        private void HabilitarCampos()
        {
            //
        }

        private void LimparCampos()
        {
            //
        }

        private void PreencherCampos()
        {
            lblFuncionario.Text = grid.CurrentRow.Cells[12].Value.ToString();
            lblCliente.Text = grid.CurrentRow.Cells[0].Value.ToString();
            lblDataVenda.Text = grid.CurrentRow.Cells[4].Value.ToString();
            lblMetodo.Text = grid.CurrentRow.Cells[11].Value.ToString();
            rtbObservacoes.Text = grid.CurrentRow.Cells[10].Value.ToString();
            txtDesconto.Text = "R$ " + Convert.ToDecimal(grid.CurrentRow.Cells[6].Value).ToString("n2");
            txtAcrescimo.Text = "R$ " + Convert.ToDecimal(grid.CurrentRow.Cells[7].Value).ToString("n2");
            txtSubtotal.Text = "R$ " + Convert.ToDecimal(grid.CurrentRow.Cells[5].Value).ToString("n2");
            txtTotal.Text = "R$ " + Convert.ToDecimal(grid.CurrentRow.Cells[8].Value).ToString("n2");

            id = Convert.ToInt32(grid.CurrentRow.Cells[1].Value);

            DataTable dt = dao.Produtos(id);

            produtos.DataSource = dt;

            FormatarGridProdutos();
        }

        private void grid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            PreencherCampos();
        }

        private void FormatarGridProdutos()
        {

            produtos.Columns[0].HeaderText = "Produto";
            produtos.Columns[1].HeaderText = "Quantidade";
            produtos.Columns[2].HeaderText = "Valor Unit.";

            //grid.Columns.Add("valor_total", "Valor Total");
            produtos.Columns[3].HeaderText = "Valor Total";

            produtos.Columns[2].DefaultCellStyle.Format = "c";
            produtos.Columns[3].DefaultCellStyle.Format = "c";

            produtos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            id = Convert.ToInt32(grid.CurrentRow.Cells[1].Value);
            FrmMessageBox mb = new FrmMessageBox();
            mb.Confirm("Deseja realmente excluir este registro?", "Confirmação", true);
            if (mb.DialogResult == DialogResult.Yes)
            {
                try
                {
                    dao.Excluir(id);

                    FrmMessageBox mb2 = new FrmMessageBox();
                    mb2.Show("Registro deletado com sucesso!", "Vendas Efetuadas", true);
                }
                catch (Exception)
                {
                    //
                }
            }

            Listar();
        }
    }
}
