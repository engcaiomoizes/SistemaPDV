using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.UserControls
{
    public partial class UCItemEstoque : UserControl
    {
        private int id;
        private string descricao;
        private UInt16 estoque;
        private UInt16 novo_valor;
        private UInt16 estoque_final;
        private int codigo;
        
        public UCItemEstoque()
        {
            InitializeComponent();
        }

        public UCItemEstoque(int id, string descricao, ushort estoque, ushort novo_valor, int codigo)
        {
            InitializeComponent();
            this.id = id;
            this.descricao = descricao;
            this.estoque = estoque;
            this.novo_valor = novo_valor;
            this.codigo = codigo;
            Preencher();
        }

        public void Atualizar(ushort novo_valor, int codigo)
        {
            this.novo_valor = novo_valor;
            this.codigo = codigo;
            Preencher();
        }
        
        public ushort GetEstoqueFinal()
        {
            return this.estoque_final;
        }

        public int GetId()
        {
            return this.id;
        }

        private void Preencher()
        {
            lblProduto.Text = descricao;
            lblEstoque.Text = estoque.ToString();
            switch (codigo)
            {
                case 0:
                    lblNovoValor.Text = estoque.ToString() + "+" + novo_valor.ToString();
                    estoque_final = (ushort)(estoque + novo_valor);
                    break;
                case 1:
                    lblNovoValor.Text = estoque.ToString() + "-" + novo_valor.ToString();
                    estoque_final = (ushort)(estoque - novo_valor);
                    break;
                case 2:
                    lblNovoValor.Text = novo_valor.ToString();
                    estoque_final = novo_valor;
                    break;
            }
            lblEstoqueFinal.Text = estoque_final.ToString();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
