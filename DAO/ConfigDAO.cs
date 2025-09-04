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
    internal class ConfigDAO
    {

        private readonly Conexao vcon;

        public ConfigDAO()
        {
            this.vcon = new Conexao();
        }

        public void Atualizar()
        {
            //
        }

        public bool ControleDeCaixa()
        {
            using (Conexao con = new Conexao())
            {
                string sql = "SELECT * FROM CONFIG WHERE CONTROLE_CAIXA = 'T'";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    using (FbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) return true;
                        return false;
                    }
                }
            }
        }

        public bool AtivarControleCaixa()
        {
            using (Conexao con = new Conexao())
            {
                string sql = "UPDATE CONFIG SET CONTROLE_CAIXA = 'T'";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public bool VerificarEstoque()
        {
            using (Conexao con = new Conexao())
            {
                string sql = "SELECT * FROM CONFIG WHERE VERIFICAR_ESTOQUE = 'T'";
                using (FbCommand cmd = new FbCommand (sql, con.GetConnection()))
                {
                    con.Open();
                    using (FbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) return true;
                        return false;
                    }
                }
            }
        }

        public bool VerificarCliente()
        {
            using (Conexao con = new Conexao())
            {
                string sql = "SELECT * FROM CONFIG WHERE CONFIRMAR_SEM_CLIENTE = 'T'";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    using (FbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) return true;
                        return false;
                    }
                }
            }
        }

        public Config ObterGeral()
        {
            Config config = null;
            using (Conexao con = new Conexao())
            {
                string sql = "SELECT * FROM CONFIG FIRST";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    using (FbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            config = new Config
                            {
                                controle_caixa = MainClass.ConvertCharToBool(Convert.ToChar(reader["CONTROLE_CAIXA"])),
                                verificar_estoque = MainClass.ConvertCharToBool(Convert.ToChar(reader["VERIFICAR_ESTOQUE"])),
                                confirmar_sem_cliente = MainClass.ConvertCharToBool(Convert.ToChar(reader["CONFIRMAR_SEM_CLIENTE"])),
                                notificacao_aniversario = MainClass.ConvertCharToBool(Convert.ToChar(reader["NOTIFICACAO_ANIVERSARIO"])),
                                notificacao_contas_pagar = MainClass.ConvertCharToBool(Convert.ToChar(reader["NOTIFICACAO_CONTAS_PAGAR"])),
                                notificacao_contas_receber = MainClass.ConvertCharToBool(Convert.ToChar(reader["NOTIFICACAO_CONTAS_RECEBER"])),
                                primary_color = Convert.ToString(reader["PRIMARY_COLOR"]),
                                secondary_color = Convert.ToString(reader["SECONDARY_COLOR"]),
                                titles_bg_color = Convert.ToString(reader["TITLES_BG_COLOR"]),
                            };
                            return config;
                        }
                    }
                }
            }
            return config;
        }

        public Loja ObterDados()
        {
            Loja loja = null;
            using (Conexao con = new Conexao())
            {
                string sql = "SELECT * FROM LOJA FIRST";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    using (FbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            loja = new Loja
                            {
                                nome_razao = Convert.ToString(reader["NOME_RAZAO"]),
                                nome_fantasia = Convert.ToString(reader["NOME_FANTASIA"]),
                                cnpj = Convert.ToString(reader["CNPJ"]),
                                telefone = Convert.ToString(reader["TELEFONE"]),
                                email = Convert.ToString(reader["EMAIL"]),
                                cep = Convert.ToString(reader["CEP"]),
                                endereco = Convert.ToString(reader["ENDERECO"]),
                                bairro = Convert.ToString(reader["BAIRRO"]),
                                complemento = Convert.ToString(reader["COMPLEMENTO"]),
                                cidade = Convert.ToString(reader["CIDADE"]),
                                uf = Convert.ToString(reader["UF"]),
                            };
                            return loja;
                        }
                    }
                }
            }
            return loja;
        }

        public bool AtualizarDados(Loja obj)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "UPDATE LOJA SET NOME_RAZAO = @nomerazao, NOME_FANTASIA = @nomefantasia, CNPJ = @cnpj, TELEFONE = @telefone, EMAIL = @email, CEP = @cep, ENDERECO = @endereco, BAIRRO = @bairro, COMPLEMENTO = @complemento, CIDADE = @cidade, UF = @uf, UPDATED_AT = @updatedat WHERE ID = @id";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    try
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@nomerazao", obj.nome_razao);
                        cmd.Parameters.AddWithValue("@nomefantasia", obj.nome_fantasia);
                        cmd.Parameters.AddWithValue("@cnpj", obj.cnpj);
                        cmd.Parameters.AddWithValue("@telefone", obj.telefone);
                        cmd.Parameters.AddWithValue("@email", obj.email);
                        cmd.Parameters.AddWithValue("@cep", obj.cep);
                        cmd.Parameters.AddWithValue("@endereco", obj.endereco);
                        cmd.Parameters.AddWithValue("@bairro", obj.bairro);
                        cmd.Parameters.AddWithValue("@complemento", obj.complemento);
                        cmd.Parameters.AddWithValue("@cidade", obj.cidade);
                        cmd.Parameters.AddWithValue("@uf", obj.uf);
                        cmd.Parameters.AddWithValue("@updatedat", DateTime.Now);
                        cmd.Parameters.AddWithValue("@id", 1);
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

        public bool AtualizarGeral(Config obj)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "UPDATE CONFIG SET CONTROLE_CAIXA = @controlecaixa, VERIFICAR_ESTOQUE = @verificarestoque, CONFIRMAR_SEM_CLIENTE = @confirmarsemcliente, NOTIFICACAO_ANIVERSARIO = @notaniversario, NOTIFICACAO_CONTAS_PAGAR = @notcontaspagar, NOTIFICACAO_CONTAS_RECEBER = @notcontasreceber, PRIMARY_COLOR = @primarycolor, SECONDARY_COLOR = @secondarycolor, TITLES_BG_COLOR = @titlesbgcolor WHERE ID = @id";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    try
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@controlecaixa", MainClass.ConvertBoolToChar(obj.controle_caixa));
                        cmd.Parameters.AddWithValue("@verificarestoque", MainClass.ConvertBoolToChar(obj.verificar_estoque));
                        cmd.Parameters.AddWithValue("@confirmarsemcliente", MainClass.ConvertBoolToChar(obj.confirmar_sem_cliente));
                        cmd.Parameters.AddWithValue("@notaniversario", MainClass.ConvertBoolToChar(obj.notificacao_aniversario));
                        cmd.Parameters.AddWithValue("@notcontaspagar", MainClass.ConvertBoolToChar(obj.notificacao_contas_pagar));
                        cmd.Parameters.AddWithValue("@notcontasreceber", MainClass.ConvertBoolToChar(obj.notificacao_contas_receber));
                        cmd.Parameters.AddWithValue("@primarycolor", obj.primary_color);
                        cmd.Parameters.AddWithValue("@secondarycolor", obj.secondary_color);
                        cmd.Parameters.AddWithValue("@titlesbgcolor", obj.titles_bg_color);
                        cmd.Parameters.AddWithValue("@id", 1);
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

    }
}
