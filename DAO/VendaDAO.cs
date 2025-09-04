using FirebirdSql.Data.FirebirdClient;
using Sistema.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.DAO
{
    internal class VendaDAO
    {

        private readonly Conexao vcon;
        private readonly ConfigDAO configDAO = new ConfigDAO();

        public VendaDAO()
        {
            this.vcon = new Conexao();
        }

        public bool Cadastrar(Venda obj)
        {
            int id;
            using (Conexao con = new Conexao())
            {
                string sql = "INSERT INTO VENDAS (CLIENTE, FUNCIONARIO, DATA_CADASTRO, SUBTOTAL, DESCONTO, ACRESCIMO, TOTAL, METODO, OBSERVACOES) VALUES (@cliente, @funcionario, @datacadastro, @subtotal, @desconto, @acrescimo, @total, @metodo, @observacoes) RETURNING ID;";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@cliente", obj.cliente);
                    cmd.Parameters.AddWithValue("@funcionario", obj.funcionario);
                    cmd.Parameters.AddWithValue("@datacadastro", obj.data_cadastro);
                    cmd.Parameters.AddWithValue("@subtotal", obj.subtotal);
                    cmd.Parameters.AddWithValue("@desconto", obj.desconto);
                    cmd.Parameters.AddWithValue("@acrescimo", obj.acrescimo);
                    cmd.Parameters.AddWithValue("@total", obj.total);
                    cmd.Parameters.AddWithValue("@metodo", obj.metodo);
                    cmd.Parameters.AddWithValue("@observacoes", obj.observacoes);
                    id = Convert.ToInt32(cmd.ExecuteScalar());
                }
                sql = "INSERT INTO VENDAS_PRODUTOS (VENDA, PRODUTO, QUANTIDADE) VALUES (@venda, @produto, @quantidade)";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@venda", id);
                    foreach (KeyValuePair<int, int> produto in obj.produtos)
                    {
                        cmd.Parameters.AddWithValue("@produto", produto.Key);
                        cmd.Parameters.AddWithValue("@quantidade", produto.Value);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.RemoveAt("@produto");
                        cmd.Parameters.RemoveAt("@quantidade");
                    }
                }
                
                if (configDAO.VerificarEstoque())
                {
                    sql = "UPDATE ESTOQUES SET QUANTIDADE = QUANTIDADE - @qtde WHERE PRODUTO = @produto";
                    using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                    {
                        foreach (KeyValuePair<int, int> produto in obj.produtos)
                        {
                            cmd.Parameters.AddWithValue("@qtde", produto.Value);
                            cmd.Parameters.AddWithValue("@produto", produto.Key);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.RemoveAt("@qtde");
                            cmd.Parameters.RemoveAt("@produto");
                        }
                    }
                }
            }

            CaixaDAO caixaDAO = new CaixaDAO();

            if (caixaDAO.ExisteCaixaAberto())
            {
                Movimento movimento = new Movimento
                {
                    created_at = DateTime.Now,
                    valor = obj.total,
                    troco = 0,
                    observacoes = obj.observacoes,
                    entrada = true,
                    metodo = obj.metodo,
                };
                if (caixaDAO.CadastrarMovimento(movimento))
                    return true;

                return false;
            }
            return true;
        }

        public void Excluir(int id)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "DELETE FROM VENDAS WHERE ID = @id";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable Listar()
        {
            using (Conexao con = new Conexao())
            {
                string sql = "SELECT c.NOME, v.*, m.NOME, f.NOME FROM VENDAS v LEFT JOIN CLIENTES c ON c.ID = v.CLIENTE INNER JOIN METODOS m ON m.ID = v.METODO INNER JOIN FUNCIONARIOS f ON f.ID = v.FUNCIONARIO ORDER BY v.TOTAL ASC";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    using (FbDataAdapter da = new FbDataAdapter())
                    {
                        da.SelectCommand = cmd;
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public DataTable Produtos(int venda)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "SELECT p.DESCRICAO, vp.QUANTIDADE, p.VALOR, (vp.QUANTIDADE * p.VALOR) AS valor_total FROM VENDAS_PRODUTOS vp INNER JOIN PRODUTOS p ON p.ID = vp.PRODUTO WHERE vp.VENDA = @venda";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@venda", venda);
                    using (FbDataAdapter da = new FbDataAdapter())
                    {
                        da.SelectCommand = cmd;
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }

    }
}
