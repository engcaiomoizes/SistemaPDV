using FirebirdSql.Data.FirebirdClient;
using Sistema.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.DAO
{
    internal class ClienteDAO
    {

        private readonly Conexao vcon;

        public ClienteDAO()
        {
            this.vcon = new Conexao();
        }

        public bool Cadastrar(Cliente obj)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "INSERT INTO CLIENTES (NOME, CPF, DATA_NASCIMENTO, TELEFONE, EMAIL, CEP, ENDERECO, BAIRRO, COMPLEMENTO, CIDADE, UF) VALUES (@nome, @cpf, @datanascimento, @telefone, @email, @cep, @endereco, @bairro, @complemento, @cidade, @uf)";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    try
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@nome", obj.nome);
                        cmd.Parameters.AddWithValue("@cpf", obj.cpf);
                        cmd.Parameters.AddWithValue("@datanascimento", obj.data_nascimento);
                        cmd.Parameters.AddWithValue("@telefone", obj.telefone);
                        cmd.Parameters.AddWithValue("@email", obj.email);
                        cmd.Parameters.AddWithValue("@cep", obj.cep);
                        cmd.Parameters.AddWithValue("@endereco", obj.endereco);
                        cmd.Parameters.AddWithValue("@bairro", obj.bairro);
                        cmd.Parameters.AddWithValue("@complemento", obj.complemento);
                        cmd.Parameters.AddWithValue("@cidade", obj.cidade);
                        cmd.Parameters.AddWithValue("@uf", obj.uf);
                        cmd.ExecuteNonQuery();
                        return true;
                    } catch (FbException ex)
                    {
                        if (ex.ErrorCode == 1062)
                        {
                            MessageBox.Show("Já existe um cliente cadastrado com este CPF!", "Atenção");
                        }
                        return false;
                    }
                }
            }
        }

        public bool Editar(Cliente obj)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "UPDATE CLIENTES SET NOME = @nome, CPF = @cpf, DATA_NASCIMENTO = @datanascimento, TELEFONE = @telefone, EMAIL = @email, CEP = @cep, ENDERECO = @endereco, BAIRRO = @bairro, COMPLEMENTO = @complemento, CIDADE = @cidade, UF = @uf WHERE ID = @id";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    try
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@nome", obj.nome);
                        cmd.Parameters.AddWithValue("@cpf", obj.cpf);
                        cmd.Parameters.AddWithValue("@datanascimento", obj.data_nascimento);
                        cmd.Parameters.AddWithValue("@telefone", obj.telefone);
                        cmd.Parameters.AddWithValue("@email", obj.email);
                        cmd.Parameters.AddWithValue("@cep", obj.cep);
                        cmd.Parameters.AddWithValue("@endereco", obj.endereco);
                        cmd.Parameters.AddWithValue("@bairro", obj.bairro);
                        cmd.Parameters.AddWithValue("@complemento", obj.complemento);
                        cmd.Parameters.AddWithValue("@cidade", obj.cidade);
                        cmd.Parameters.AddWithValue("@uf", obj.uf);
                        cmd.Parameters.AddWithValue("@id", obj.id);
                        cmd.ExecuteNonQuery();
                        return true;
                    } catch (FbException ex)
                    {
                        if (ex.ErrorCode == 1062)
                        {
                            MessageBox.Show("Já existe um cliente cadastrado com este CPF!", "Atenção");
                        }
                        return false;
                    }
                }
            }
        }

        public void Excluir(int id)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "DELETE FROM CLIENTES WHERE ID = @id";
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
                string sql = "SELECT * FROM CLIENTES ORDER BY NOME ASC";
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

        public DataTable BuscarPorNome(string str)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "SELECT * FROM CLIENTES WHERE NOME LIKE '%" + str + "%' ORDER BY NOME ASC";
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

        public DataTable BuscarListaPorCPF(string cpf)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "SELECT * FROM CLIENTES WHERE CPF = @cpf ORDER BY NOME ASC";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@cpf", cpf);
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

        public Cliente BuscarPorCPF(string cpf)
        {
            Cliente cliente = null;
            using (Conexao con = new Conexao())
            {
                string sql = "SELECT * FROM CLIENTES WHERE CPF = @cpf ORDER BY NOME ASC";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@cpf", cpf);
                    using (FbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cliente = new Cliente
                            {
                                id = Convert.ToInt32(reader["ID"]),
                                nome = Convert.ToString(reader["NOME"]),
                                cpf = Convert.ToString(reader["CPF"]),
                                telefone = Convert.ToString(reader["TELEFONE"]),
                                email = Convert.ToString(reader["EMAIL"]),
                                cep = Convert.ToString(reader["CEP"]),
                                endereco = Convert.ToString(reader["ENDERECO"]),
                                bairro = Convert.ToString(reader["BAIRRO"]),
                                complemento = Convert.ToString(reader["COMPLEMENTO"]),
                                cidade = Convert.ToString(reader["CIDADE"]),
                                uf = Convert.ToString(reader["UF"]),
                            };
                        }
                    }
                }
            }
            return cliente;
        }

        public bool ExisteCliente(string cpf)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "SELECT * FROM CLIENTES WHERE CPF = @cpf";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@cpf", cpf);
                    using (FbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) return true;
                        return false;
                    }
                }
            }
        }

    }
}
