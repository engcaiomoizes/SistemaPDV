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
    internal class ProdutoDAO
    {
        
        private readonly Conexao vcon;
        
        public ProdutoDAO()
        {
            this.vcon = new Conexao();
        }

        public bool Cadastrar(Produto obj)
        {
            using (Conexao con = new Conexao())
            {
                int produto = 0;
                string sql = "INSERT INTO PRODUTOS (BARCODE, DESCRICAO, EMBALAGEM, FORNECEDOR, ESTOQUE_MIN, CONTROLAR_ESTOQUE, CUSTO, VALOR, DATA_CADASTRO, ATIVO) VALUES (@barcode, @descricao, @embalagem, @fornecedor, @estoquemin, @estoque, @custo, @valor, @datacadastro, @ativo) RETURNING ID;";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    try
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@barcode", obj.barcode);
                        cmd.Parameters.AddWithValue("@descricao", obj.descricao);
                        cmd.Parameters.AddWithValue("@embalagem", obj.embalagem);
                        cmd.Parameters.AddWithValue("@fornecedor", obj.fornecedor);
                        cmd.Parameters.AddWithValue("@estoquemin", obj.estoque_min);
                        cmd.Parameters.AddWithValue("@estoque", MainClass.ConvertBoolToChar(obj.controlar_estoque));
                        cmd.Parameters.AddWithValue("@custo", obj.custo);
                        cmd.Parameters.AddWithValue("@valor", obj.valor);
                        cmd.Parameters.AddWithValue("@datacadastro", obj.data_cadastro);
                        cmd.Parameters.AddWithValue("@ativo", MainClass.ConvertBoolToChar(obj.ativo));

                        if (obj.estoque == 0)
                        {
                            cmd.ExecuteNonQuery();
                            return true;
                        }

                        produto = Convert.ToInt32(cmd.ExecuteScalar());
                        if (CadastrarEstoqueInicial(obj.estoque, produto))
                            return true;

                        return false;
                    } catch (FbException e)
                    {
                        if (e.ErrorCode == 1062)
                            MessageBox.Show("Já existe um produto com este código de barras!", "Atenção");
                        return false;
                    }
                }
            }
        }

        public bool Editar(Produto obj)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "UPDATE PRODUTOS SET BARCODE = @barcode, DESCRICAO = @descricao, EMBALAGEM = @embalagem, FORNECEDOR = @fornecedor, ESTOQUE_MIN = @estoquemin, CONTROLAR_ESTOQUE = @estoque, CUSTO = @custo, VALOR = @valor, DATA_CADASTRO = @datacadastro, ATIVO = @ativo WHERE ID = @id";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    try
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@barcode", obj.barcode);
                        cmd.Parameters.AddWithValue("@descricao", obj.descricao);
                        cmd.Parameters.AddWithValue("@embalagem", obj.embalagem);
                        cmd.Parameters.AddWithValue("@fornecedor", obj.fornecedor);
                        cmd.Parameters.AddWithValue("@estoquemin", obj.estoque_min);
                        cmd.Parameters.AddWithValue("@estoque", MainClass.ConvertBoolToChar(obj.controlar_estoque));
                        cmd.Parameters.AddWithValue("@custo", obj.custo);
                        cmd.Parameters.AddWithValue("@valor", obj.valor);
                        cmd.Parameters.AddWithValue("@datacadastro", obj.data_cadastro);
                        cmd.Parameters.AddWithValue("@ativo", MainClass.ConvertBoolToChar(obj.ativo));
                        cmd.Parameters.AddWithValue("@id", obj.id);
                        cmd.ExecuteNonQuery();
                        return true;
                    } catch (FbException e)
                    {
                        if (e.ErrorCode == 1062)
                            MessageBox.Show("Já existe um produto com este código de barras!", "Atenção");
                        return false;
                    }
                }
            }
        }

        public void Excluir(int id)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "DELETE FROM PRODUTOS WHERE ID = @id";
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
                string sql = "SELECT p.ID, p.BARCODE, p.DESCRICAO, p.EMBALAGEM, f.NOME, p.ESTOQUE_MIN, COALESCE(e.QUANTIDADE, 0) as estoque, p.CUSTO, p.VALOR, p.DATA_CADASTRO, p.ATIVO, p.FORNECEDOR, c.DESCRICAO as cDescricao, p.CATEGORIA, p.CONTROLAR_ESTOQUE FROM PRODUTOS p INNER JOIN FORNECEDORES f ON f.ID = p.FORNECEDOR LEFT OUTER JOIN CATEGORIAS c ON c.ID = p.CATEGORIA LEFT OUTER JOIN ESTOQUES e ON e.PRODUTO = p.ID ORDER BY p.DESCRICAO ASC";
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

        public Produto BuscarPorBarcode(string barcode)
        {
            Produto produto = null;
            using (Conexao con = new Conexao())
            {
                string sql = "SELECT p.*, e.QUANTIDADE as estoque FROM PRODUTOS p LEFT OUTER JOIN ESTOQUES e ON e.PRODUTO = p.ID WHERE p.BARCODE = @barcode";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@barcode", barcode);
                    using (FbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            produto = new Produto
                            {
                                id = Convert.ToInt32(reader["ID"]),
                                descricao = Convert.ToString(reader["DESCRICAO"]),
                                valor = Convert.ToDecimal(reader["VALOR"]),
                                barcode = Convert.ToString(reader["BARCODE"]),
                                ativo = MainClass.ConvertCharToBool(Convert.ToChar(reader["ATIVO"])),
                            };

                            if (reader["estoque"] != DBNull.Value)
                                produto.estoque = Convert.ToUInt16(reader["estoque"]);
                        }
                    }
                }
            }
            return produto;
        }

        public Produto BuscarPorId(int id)
        {
            Produto produto = null;
            using (Conexao con = new Conexao())
            {
                string sql = "SELECT p.*, e.QUANTIDADE as estoque FROM PRODUTOS p LEFT OUTER JOIN ESTOQUES e ON e.PRODUTO = p.ID WHERE p.ID = @id";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    using (FbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            produto = new Produto
                            {
                                id = Convert.ToInt32(reader["ID"]),
                                descricao = Convert.ToString(reader["DESCRICAO"]),
                                valor = Convert.ToDecimal(reader["VALOR"]),
                                barcode = Convert.ToString(reader["BARCODE"]),
                                ativo = MainClass.ConvertCharToBool(Convert.ToChar(reader["ATIVO"])),
                            };

                            if (reader["estoque"] != DBNull.Value)
                                produto.estoque = Convert.ToUInt16(reader["estoque"]);
                        }
                    }
                }
            }
            return produto;
        }

        public bool CadastrarEstoqueInicial(int qtde, int produto)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "INSERT INTO ESTOQUES (PRODUTO, QUANTIDADE) VALUES (@produto, @quantidade)";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    try
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@produto", produto);
                        cmd.Parameters.AddWithValue("@quantidade", qtde);
                        cmd.ExecuteNonQuery();
                        return true;
                    } catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                        return false;
                    }
                }
            }
        }

        public bool SomarEstoque(int qtde, int produto)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "UPDATE ESTOQUES SET QUANTIDADE = QUANTIDADE + @qtde WHERE PRODUTO = @produto";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    try
                    {
                        cmd.Parameters.AddWithValue("@qtde", qtde);
                        cmd.Parameters.AddWithValue("@produto", produto);
                        cmd.ExecuteNonQuery();
                        return true;
                    } catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                        return false;
                    }
                }
            }
        }

        public bool SubtrairEstoque(int qtde, int produto)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "UPDATE ESTOQUES SET QUANTIDADE = QUANTIDADE - @qtde WHERE PRODUTO = @produto";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    try
                    {
                        cmd.Parameters.AddWithValue("@qtde", qtde);
                        cmd.Parameters.AddWithValue("@produto", produto);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                        return false;
                    }
                }
            }
        }

        public bool CorrigirEstoque(int qtde, int produto)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "UPDATE ESTOQUES SET QUANTIDADE = @qtde WHERE PRODUTO = @produto";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    try
                    {
                        cmd.Parameters.AddWithValue("@qtde", qtde);
                        cmd.Parameters.AddWithValue("@produto", produto);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                        return false;
                    }
                }
            }
        }

        public bool AtualizarEstoques(Dictionary<int, UInt16> produtos)
        {
            using (Conexao con = new Conexao())
            {
                bool found;
                foreach (int produto in produtos.Keys)
                {
                    string sql = "SELECT * FROM ESTOQUES WHERE PRODUTO = @produto";
                    using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@produto", produto);
                        using (FbDataReader reader = cmd.ExecuteReader())
                        {
                            found = reader.HasRows;
                        }
                    }
                    if (!found)
                    {
                        sql = "INSERT INTO ESTOQUES (PRODUTO, QUANTIDADE) VALUES (@produto, @qtde)";
                        using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                        {
                            try
                            {
                                cmd.Parameters.AddWithValue("@produto", produto);
                                cmd.Parameters.AddWithValue("@qtde", 0);
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Erro: " + ex.Message);
                                return false;
                            }
                        }
                    }
                }
            }

            using (Conexao con = new Conexao()) {
                string sql = "UPDATE ESTOQUES SET QUANTIDADE = @qtde WHERE PRODUTO = @produto";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    try
                    {
                        con.Open();
                        foreach (KeyValuePair<int, UInt16> kvp in produtos)
                        {
                            cmd.Parameters.AddWithValue("@qtde", kvp.Value);
                            cmd.Parameters.AddWithValue("@produto", kvp.Key);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.RemoveAt("@qtde");
                            cmd.Parameters.RemoveAt("@produto");
                        }
                        return true;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Erro: " + e.Message);
                        return false;
                    }
                }
            }
        }

    }
}
