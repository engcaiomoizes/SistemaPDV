using FirebirdSql.Data.FirebirdClient;
using Sistema.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.DAO
{
    internal class CaixaDAO
    {

        public CaixaDAO()
        {
            //
        }

        public bool AbrirCaixa(string apelido, decimal valor)
        {
            if (!ExisteCaixaAberto())
            {
                using (Conexao con = new Conexao())
                {
                    string sql = "INSERT INTO CAIXAS (DATA_ABERTURA, STATUS, APELIDO) VALUES (@dataabertura, @status, @apelido) RETURNING ID;";
                    using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@dataabertura", DateTime.Now);
                        cmd.Parameters.AddWithValue("@status", 'T');
                        cmd.Parameters.AddWithValue("@apelido", apelido);
                        int id = Convert.ToInt32(cmd.ExecuteScalar());

                        if (valor > 0)
                        {
                            Transacao transacao = new Transacao
                            {
                                caixa = id,
                                valor = valor,
                                metodo = null,
                                entrada = true,
                                descricao = "Valor de abertura",
                                created_at = DateTime.Now,
                            };

                            if (CadastrarTransacao(transacao))
                                return true;

                            return false;
                        }

                        return true;
                    }
                }
            } else
            {
                MessageBox.Show("Já existe um caixa aberto!");
                return false;
            }
        }

        public bool FecharCaixa()
        {
            if (ExisteCaixaAberto())
            {
                Caixa caixa = ObterCaixaAberto();
                using (Conexao con = new Conexao())
                {
                    string sql = "UPDATE CAIXAS SET STATUS = @status, DATA_FECHAMENTO = @datafechamento WHERE ID = @id";
                    using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@status", 0);
                        cmd.Parameters.AddWithValue("@datafechamento", DateTime.Now);
                        cmd.Parameters.AddWithValue("@id", caixa.id);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            } else
            {
                MessageBox.Show("Não existe nenhum caixa aberto!");
                return false;
            }
        }

        public void AlterarApelido(int id, string apelido)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "UPDATE CAIXAS SET APELIDO = @apelido WHERE ID = @id";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@apelido", apelido);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool ExisteCaixaAberto()
        {
            using (Conexao con = new Conexao())
            {
                string sql = "SELECT * FROM CAIXAS WHERE STATUS = 'T'";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    using (FbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
        }

        public bool CaixaEstaAberto(int id)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "SELECT * FROM CAIXAS WHERE STATUS = 'T' AND ID = @id";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    using (FbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                            return true;
                        return false;
                    }
                }
            }
        }

        public bool CadastrarTransacao(Transacao obj)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "INSERT INTO TRANSACOES (CAIXA, VALOR, METODO, ENTRADA, DESCRICAO, CREATED_AT) VALUES (@caixa, @valor, @metodo, @entrada, @descricao, @createdat)";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@caixa", obj.caixa);
                    cmd.Parameters.AddWithValue("@valor", obj.valor);
                    cmd.Parameters.AddWithValue("@metodo", obj.metodo);
                    cmd.Parameters.AddWithValue("@entrada", MainClass.ConvertBoolToChar(obj.entrada));
                    cmd.Parameters.AddWithValue("@descricao", obj.descricao);
                    cmd.Parameters.AddWithValue("@createdat", obj.created_at);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public void EditarTransacao(Transacao obj)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "UPDATE TRANSACOES SET VALOR = @valor, METODO = @metodo, ENTRADA = @entrada, DESCRICAO = @descricao, UPDATED_AT = @updatedat WHERE ID = @id";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@valor", obj.valor);
                    cmd.Parameters.AddWithValue("@metodo", obj.metodo);
                    cmd.Parameters.AddWithValue("@entrada", MainClass.ConvertBoolToChar(obj.entrada));
                    cmd.Parameters.AddWithValue("@descricao", obj.descricao);
                    cmd.Parameters.AddWithValue("@updatedat", obj.updated_at);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ExcluirTransacao(int id)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "DELETE FROM TRANSACOES WHERE ID = @id";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Transacao> GetTransacoes(int caixa)
        {
            List<Transacao> transacoes = new List<Transacao>();
            using (Conexao con = new Conexao())
            {
                string sql = "SELECT * FROM TRANSACOES WHERE CAIXA = @caixa";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@caixa", caixa);
                    using (FbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Transacao transacao = new Transacao
                            {
                                id = Convert.ToInt32(reader["ID"]),
                                caixa = Convert.ToInt32(reader["CAIXA"]),
                                valor = Convert.ToDecimal(reader["VALOR"]),
                                metodo = reader.IsDBNull(reader.GetOrdinal("METODO")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("METODO")),
                                entrada = MainClass.ConvertCharToBool(Convert.ToChar(reader["ENTRADA"])),
                                descricao = Convert.ToString(reader["DESCRICAO"]),
                                created_at = Convert.ToDateTime(reader["CREATED_AT"]),
                            };

                            if (reader["UPDATED_AT"] != DBNull.Value)
                                transacao.updated_at = Convert.ToDateTime(reader["UPDATED_AT"]);

                            transacoes.Add(transacao);
                        }
                        return transacoes;
                    }
                }
            }
        }

        public Caixa ObterCaixaAberto()
        {
            Caixa caixa = null;
            using (Conexao con = new Conexao())
            {
                string sql = "SELECT * FROM CAIXAS WHERE STATUS = 'T'";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    using (FbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            caixa = new Caixa
                            {
                                id = Convert.ToInt32(reader["ID"]),
                                data_abertura = Convert.ToDateTime(reader["DATA_ABERTURA"]),
                                status = MainClass.ConvertCharToBool(Convert.ToChar(reader["STATUS"])),
                                apelido = Convert.ToString(reader["APELIDO"]),
                                transacoes = GetTransacoes(Convert.ToInt32(reader["ID"])),
                            };

                            if (reader["DATA_FECHAMENTO"] != DBNull.Value)
                                caixa.data_fechamento = Convert.ToDateTime(reader["DATA_FECHAMENTO"]);

                            if (reader["ULTIMA_ATUALIZACAO"] != DBNull.Value)
                                caixa.ultima_atualizacao = Convert.ToDateTime(reader["ULTIMA_ATUALIZACAO"]);
                        }
                    }
                }
            }

            return caixa;
        }

        public bool CadastrarMovimento(Movimento obj)
        {
            if (ExisteCaixaAberto()) {
                Caixa caixa = ObterCaixaAberto();
                using (Conexao con = new Conexao())
                {
                    string sql = "INSERT INTO MOVIMENTOS (CREATED_AT, CAIXA, VALOR, TROCO, OBSERVACOES, ENTRADA, METODO) VALUES (@createdat, @caixa, @valor, @troco, @observacoes, @entrada, @metodo)";
                    using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@createdat", obj.created_at);
                        cmd.Parameters.AddWithValue("@caixa", caixa.id);
                        cmd.Parameters.AddWithValue("@valor", obj.valor);
                        cmd.Parameters.AddWithValue("@troco", obj.troco);
                        cmd.Parameters.AddWithValue("@observacoes", obj.observacoes);
                        cmd.Parameters.AddWithValue("@entrada", MainClass.ConvertBoolToChar(obj.entrada));
                        cmd.Parameters.AddWithValue("@metodo", obj.metodo);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            return false;
        }

        public int ObterNumeroTotalMovimentos()
        {
            if (ExisteCaixaAberto())
            {
                int total = 0;
                Caixa caixa = ObterCaixaAberto();
                using (Conexao con = new Conexao())
                {
                    string sql = "SELECT COUNT(*) as total FROM MOVIMENTOS WHERE CAIXA = @caixa";
                    using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@caixa", caixa.id);
                        using (FbDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                total = Convert.ToInt32(reader["total"]);
                            }
                        }
                    }
                }
                return total;
            }
            return 0;
        }

        public decimal ObterValorTotalMovimentos()
        {
            if (ExisteCaixaAberto())
            {
                decimal total = 0;
                Caixa caixa = ObterCaixaAberto();
                using (Conexao con = new Conexao())
                {
                    string sql = "SELECT SUM(VALOR) as total FROM MOVIMENTOS WHERE CAIXA = @caixa";
                    using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@caixa", caixa.id);
                        using (FbDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader["total"] != DBNull.Value)
                                    total = Convert.ToDecimal(reader["total"]);
                            }
                        }
                    }
                }
                return total;
            }
            return 0;
        }

        public decimal ObterValorTotalPorMetodo(int metodo)
        {
            decimal valor = 0;
            using (Conexao con = new Conexao())
            {
                string sql = "SELECT SUM(m.VALOR) as total FROM MOVIMENTOS m INNER JOIN CAIXAS c ON c.ID = m.CAIXA WHERE m.METODO = @metodo AND c.STATUS = 'T'";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@metodo", metodo);
                    using (FbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["total"] != DBNull.Value)
                                valor = Convert.ToDecimal(reader["total"]);
                        }
                    }
                }
            }
            return valor;
        }

        public int ObterQuantidadeMetodosDistintos()
        {
            int total = 0;
            if (ExisteCaixaAberto())
            {
                Caixa caixa = ObterCaixaAberto();
                using (Conexao con = new Conexao())
                {
                    string sql = "SELECT COUNT(DISTINCT METODO) as total FROM MOVIMENTOS WHERE CAIXA = @caixa";
                    using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@caixa", caixa.id);
                        using (FbDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                total = Convert.ToInt32(reader["total"]);
                            }
                        }
                    }
                }
            }
            return total;
        }

    }
}
