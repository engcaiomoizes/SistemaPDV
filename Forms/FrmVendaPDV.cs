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
    public partial class FrmVendaPDV : Form
    {
        private readonly VendaDAO dao = new VendaDAO();
        private readonly ProdutoDAO produtoDAO = new ProdutoDAO();
        private readonly ClienteDAO clienteDAO = new ClienteDAO();
        private readonly FuncionarioDAO funcionarioDAO = new FuncionarioDAO();
        private readonly ConfigDAO configDAO = new ConfigDAO();
        private string cpf;
        private Venda venda;
        private Produto produto;
        private DataTable dt;
        
        public FrmVendaPDV()
        {
            InitializeComponent();
        }

        public void setCPF(string cpf)
        {
            this.cpf = cpf;
        }

        private void FrmVendaPDV_Load(object sender, EventArgs e)
        {
            venda = new Venda();

            if (cpf == null)
            {
                venda.cliente = null;
                lblCliente.Text = "Nenhum";
            } else
            {
                Cliente cliente = clienteDAO.BuscarPorCPF(cpf);
                venda.cliente = cliente.id;
                lblCliente.Text = cliente.nome;
            }

            /* ========================================================================== */

            Funcionario funcionario = funcionarioDAO.GetByLogin(Program.LoginUsuario);
            lblFuncionario.Text = Program.NomeUsuario;
            venda.funcionario = funcionario.id;

            venda.produtos = new Dictionary<int, int>();

            PreencherValores();

            dt = new DataTable();
            dt.Columns.Add("produto_id", typeof(int));
            dt.Columns.Add("barcode", typeof(string));
            dt.Columns.Add("descricao", typeof(string));
            dt.Columns.Add("quantidade", typeof(int));
            dt.Columns.Add("valor_unitario", typeof(double));
            dt.Columns.Add("subtotal", typeof(double));
            grid.DataSource = dt;
            FormatarGrid();
            txtBarcode.Focus();
        }

        private void FormatarGrid()
        {
            grid.Columns[0].Visible = false;
            
            grid.Columns[1].HeaderText = "Cód. Barras";
            grid.Columns[2].HeaderText = "Produto";
            grid.Columns[3].HeaderText = "Qtde.";
            grid.Columns[4].HeaderText = "Valor Unit.";
            grid.Columns[5].HeaderText = "Subtotal";

            grid.Columns[4].DefaultCellStyle.Format = "c";
            grid.Columns[5].DefaultCellStyle.Format = "c";

            grid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            grid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            grid.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grid.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            grid.Columns[2].Width = 340;
            grid.Columns[3].Width = 45;
        }
        
        private void PreencherValores()
        {
            txtSubtotal.Text = "R$ " + venda.subtotal.ToString("n2");
            txtDesconto.Text = venda.desconto.ToString("n2");
            txtAcrescimo.Text = venda.acrescimo.ToString("n2");
            venda.total = venda.subtotal - venda.desconto + venda.acrescimo;
            txtTotal.Text = "R$ " + venda.total.ToString("n2");
        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                produto = produtoDAO.BuscarPorBarcode(txtBarcode.Text);
                if (produto != null)
                {
                    txtProduto.Text = produto.descricao;
                    txtQuantidade.Text = "1";
                    txtQuantidade.Focus();
                } else
                {
                    FrmMessageBox mb = new FrmMessageBox();
                    mb.Show("Produto não encontrado!", "Busca de Produto");
                }
            }
        }

        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (produto != null)
                {
                    UInt16 quantidade = Convert.ToUInt16(txtQuantidade.Text);

                    if (venda.produtos.ContainsKey(produto.id))
                    {
                        if ((quantidade > (produto.estoque - venda.produtos[produto.id])) && (configDAO.VerificarEstoque()))
                        {
                            FrmMessageBox mb = new FrmMessageBox();
                            mb.Show("Produto sem estoque suficiente!", "Estoque");
                            return;
                        }
                    } else
                    {
                        if ((quantidade > produto.estoque) && (configDAO.VerificarEstoque()))
                        {
                            FrmMessageBox mb = new FrmMessageBox();
                            mb.Show("Produto sem estoque suficiente!", "Estoque");
                            return;
                        }
                    }

                    if (!venda.produtos.ContainsKey(produto.id))
                        venda.produtos.Add(produto.id, quantidade);
                    else
                        venda.produtos[produto.id] = venda.produtos[produto.id] + quantidade;

                    venda.subtotal += (quantidade * produto.valor);
                    //txtTotal.Text = venda.subtotal.ToString();
                    PreencherValores();

                    txtBarcode.Clear();
                    txtProduto.Clear();
                    txtQuantidade.Clear();
                    txtBarcode.Focus();
                    dt.Rows.Add(produto.id, produto.barcode, produto.descricao, quantidade, produto.valor, quantidade * produto.valor);
                    grid.DataSource = dt;
                } else
                {
                    //
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            FrmMessageBox mb = new FrmMessageBox();
            mb.Confirm("Deseja realmente cancelar a venda?", "Cancelar Venda", false);
            if (mb.DialogResult == DialogResult.Yes)
            {
                Close();
                FrmPrincipal.Instance.Resetar();
            }
        }

        private void PreencherCampos()
        {
            txtDescricao.Text = grid.CurrentRow.Cells[2].Value.ToString();
            txtQtde.Text = grid.CurrentRow.Cells[3].Value.ToString();
            txtValorUnitario.Text = grid.CurrentRow.Cells[4].Value.ToString();
            txtValorTotal.Text = grid.CurrentRow.Cells[5].Value.ToString();
        }

        private void grid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            PreencherCampos();
            txtQtde.ReadOnly = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (grid.Rows.Count > 0)
            {
                FrmMessageBox mb = new FrmMessageBox();
                mb.Confirm("Remover Produto?", "Confirmação", false);
                if (mb.DialogResult == DialogResult.Yes)
                {
                    int key = Convert.ToInt32(grid.CurrentRow.Cells[0].Value);
                    int qtde = Convert.ToInt32(grid.CurrentRow.Cells[3].Value);
                    // Caso a quantidade do produto removido seja menor do que a quantidade total do produto existente
                    // na compra, remove a quantidade selecionada, senão remove completamente o produto
                    if (qtde < venda.produtos[key])
                    {
                        venda.produtos[key] -= qtde;
                    }
                    else
                    {
                        venda.produtos.Remove(key);
                    }

                    venda.subtotal -= Convert.ToDecimal(grid.CurrentRow.Cells[5].Value);
                    PreencherValores();

                    dt.Rows.RemoveAt(grid.CurrentRow.Index);
                }
            }
        }

        private void txtQtde_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                int id = Convert.ToInt32(grid.CurrentRow.Cells[0].Value);
                int qtde = Convert.ToInt32(grid.CurrentRow.Cells[3].Value);
                int novaQtde = Convert.ToInt32(txtQtde.Text);

                Produto produto = produtoDAO.BuscarPorId(id);

                if (((qtde < venda.produtos[id]) && ((venda.produtos[id] - qtde + novaQtde) > produto.estoque)) && (configDAO.VerificarEstoque()))
                {
                    FrmMessageBox mb = new FrmMessageBox();
                    mb.Show("Produto sem estoque suficiente!", "Estoque");
                    return;
                } else if ((novaQtde > produto.estoque) && (configDAO.VerificarEstoque()))
                {
                    FrmMessageBox mb = new FrmMessageBox();
                    mb.Show("Produto sem estoque suficiente!", "Estoque");
                    return;
                }

                venda.subtotal -= (venda.produtos[id] * produto.valor);
                if (qtde < venda.produtos[id])
                {
                    venda.produtos[id] -= qtde;
                    venda.produtos[id] += novaQtde;
                } else
                {
                    venda.produtos[id] = novaQtde;
                }
                grid.CurrentRow.Cells[3].Value = novaQtde;
                grid.CurrentRow.Cells[5].Value = novaQtde * produto.valor;
                venda.subtotal += (venda.produtos[id] * produto.valor);
                PreencherValores();
            }
        }

        private void btnDesconto_Click(object sender, EventArgs e)
        {
            FrmDesconto desc = new FrmDesconto();
            desc.Desconto(venda.subtotal, venda.desconto);
            MainClass.BlurBackground(desc);
            if (desc.DialogResult == DialogResult.OK)
            {
                venda.desconto = desc.valor;
                PreencherValores();
            }
        }

        private void btnAcrescimo_Click(object sender, EventArgs e)
        {
            FrmDesconto desc = new FrmDesconto();
            desc.Acrescimo(venda.subtotal, venda.acrescimo);
            MainClass.BlurBackground(desc);
            if (desc.DialogResult == DialogResult.OK)
            {
                venda.acrescimo = desc.valor;
                PreencherValores();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (venda.produtos.Count == 0)
            {
                FrmMessageBox mb = new FrmMessageBox();
                mb.Show("Nenhum item registrado na venda!", "Atenção", true);
            } else
            {
                FrmFinalizar finalizar = new FrmFinalizar();
                finalizar.ExibirTotal(venda.total);
                MainClass.BlurBackground(finalizar);
                if (finalizar.DialogResult == DialogResult.OK)
                {
                    venda.metodo = finalizar.metodo;
                    venda.observacoes = finalizar.observacoes;
                    venda.data_cadastro = DateTime.Now;
                    if (dao.Cadastrar(venda))
                    {
                        FrmMessageBox mb = new FrmMessageBox();
                        mb.Show("Venda cadastrada com sucesso!", "Sucesso", true);
                        FrmPrincipal.Instance.FecharForm();
                        //Close();
                    } else
                    {
                        //
                    }
                }
            }
        }

        private void AlterarCliente()
        {
            FrmMessageBox mb = new FrmMessageBox();
            mb.Cliente("Alterar Cliente", true);
            if (mb.DialogResult == DialogResult.OK)
            {
                setCPF(mb.cpf);
                if (cpf == null)
                {
                    venda.cliente = null;
                    lblCliente.Text = "Nenhum";
                }
                else
                {
                    Cliente cliente = clienteDAO.BuscarPorCPF(cpf);
                    venda.cliente = cliente.id;
                    lblCliente.Text = cliente.nome;
                }
            }
            else
            {
                //
            }
        }

        private void lblCliente_Click(object sender, EventArgs e)
        {
            AlterarCliente();
        }

        private void FrmVendaPDV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                AlterarCliente();
            }
        }

        private void btnAlterarCliente_Click(object sender, EventArgs e)
        {
            AlterarCliente();
        }
    }
}
