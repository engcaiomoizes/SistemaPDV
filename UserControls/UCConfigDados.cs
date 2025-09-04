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

namespace Sistema.UserControls
{
    public partial class UCConfigDados : UserControl
    {
        private readonly ConfigDAO dao = new ConfigDAO();
        
        public UCConfigDados()
        {
            InitializeComponent();
        }

        private void UCConfigDados_Load(object sender, EventArgs e)
        {
            PreencherDados();
        }

        private void PreencherDados()
        {
            Loja loja = dao.ObterDados();

            txtRazao.Text = loja.nome_razao;
            txtFantasia.Text = loja.nome_fantasia;
            mtxtCNPJ.Text = loja.cnpj;
            mtxtTelefone.Text = loja.telefone;
            txtEmail.Text = loja.email;
            mtxtCEP.Text = loja.cep;
            txtEndereco.Text = loja.endereco;
            txtBairro.Text = loja.bairro;
            txtComplemento.Text = loja.complemento;
            txtCidade.Text = loja.cidade;
            txtUF.Text = loja.uf;
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

        private void mtxtCEP_Leave(object sender, EventArgs e)
        {
            LocalizarCEP();
        }
    }
}
