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
    public partial class FrmEstoque : Form
    {
        private readonly ProdutoDAO produtoDAO = new ProdutoDAO();

        private Dictionary<int, UInt16> produtos;
        private Produto produto;

        private int height;
        
        public FrmEstoque()
        {
            InitializeComponent();
        }

        private void FrmEstoque_Load(object sender, EventArgs e)
        {
            cbbOpcao.SelectedIndex = 0;
            produtos = new Dictionary<int, UInt16>();
            height = 0;
            txtBarcode.Focus();
        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                produto = produtoDAO.BuscarPorBarcode(txtBarcode.Text);

                if (produto != null )
                {
                    txtProduto.Text = produto.descricao;
                    cbbOpcao.Enabled = true;
                    txtValor.Enabled = true;
                    btnIncluir.Enabled = true;
                    txtValor.Text = "0";
                    txtValor.Focus();
                } else
                {
                    txtProduto.Clear();
                    cbbOpcao.Enabled = false;
                    txtValor.Enabled = false;
                    btnIncluir.Enabled = false;
                    txtValor.Clear();
                    //cbbOpcao.SelectedIndex = 0;
                }

                //grid.Rows.Add(produto.descricao, produto.estoque, null, null, 1, 1);
            }
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            ushort novoValor = Convert.ToUInt16(txtValor.Text);
            if (produtos.ContainsKey(produto.id))
            {
                UserControls.UCItemEstoque uc = null;
                foreach (UserControls.UCItemEstoque control in pnlGrid.Controls)
                {
                    if (control.GetId() == produto.id)
                    {
                        uc = control;
                        break;
                    }
                }
                if (uc != null)
                {
                    uc.Atualizar(novoValor, cbbOpcao.SelectedIndex);
                    produtos[produto.id] = uc.GetEstoqueFinal();
                }
            } else
            {
                UserControls.UCItemEstoque uc = new UserControls.UCItemEstoque(
                    produto.id,
                    produto.descricao,
                    produto.estoque,
                    novoValor,
                    cbbOpcao.SelectedIndex
                );
                uc.Disposed += new EventHandler((s, ev)=>Uc_Disposed(s, ev, uc.GetId()));
                uc.Location = new Point(0, height);
                height += 40;
                pnlGrid.Controls.Add(uc);
                produtos.Add(produto.id, uc.GetEstoqueFinal());
            }
        }

        private void Uc_Disposed(object sender, EventArgs e, int id)
        {
            //throw new NotImplementedException();
            produtos.Remove(id);
            height -= 40;
            int aux = 0;
            foreach (Control control in pnlGrid.Controls)
            {
                control.Location = new Point(0, aux);
                aux += 40;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (produtoDAO.AtualizarEstoques(produtos))
            {
                MessageBox.Show("Sucesso!");
            } else
            {
                MessageBox.Show("Erro!");
            }
        }
    }
}
