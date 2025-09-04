using FirebirdSql.Data.FirebirdClient;
using Sistema.Model;
using System;
using System.Data;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Runtime.ConstrainedExecution;

namespace Sistema.DAO
{
    internal class FuncionarioDAO
    {

        private readonly Conexao vcon;

        public FuncionarioDAO()
        {
            this.vcon = new Conexao();
        }

        public void Cadastrar(Funcionario obj)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "INSERT INTO FUNCIONARIOS (NOME, CPF, TELEFONE, EMAIL, CEP, ENDERECO, BAIRRO, COMPLEMENTO, CIDADE, UF, DATA_ADMISSAO, CARGO, ATIVO) VALUES (@nome, @cpf, @telefone, @email, @cep, @endereco, @bairro, @complemento, @cidade, @uf, @dataadmissao, @cargo, @ativo)";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@nome", obj.nome);
                    cmd.Parameters.AddWithValue("@cpf", obj.cpf);
                    cmd.Parameters.AddWithValue("@telefone", obj.telefone);
                    cmd.Parameters.AddWithValue("@email", obj.email);
                    cmd.Parameters.AddWithValue("@cep", obj.cep);
                    cmd.Parameters.AddWithValue("@endereco", obj.endereco);
                    cmd.Parameters.AddWithValue("@bairro", obj.bairro);
                    cmd.Parameters.AddWithValue("@complemento", obj.complemento);
                    cmd.Parameters.AddWithValue("@cidade", obj.cidade);
                    cmd.Parameters.AddWithValue("@uf", obj.uf);
                    cmd.Parameters.AddWithValue("@dataadmissao", obj.data_admissao);
                    cmd.Parameters.AddWithValue("@cargo", obj.cargo);
                    cmd.Parameters.AddWithValue("@ativo", MainClass.ConvertBoolToChar(obj.ativo));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Editar(Funcionario obj)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "UPDATE FUNCIONARIOS SET NOME = @nome, CPF = @cpf, TELEFONE = @telefone, EMAIL = @email, CEP = @cep, ENDERECO = @endereco, BAIRRO = @bairro, COMPLEMENTO = @complemento, CIDADE = @cidade, UF = @uf, DATA_ADMISSAO = @dataadmissao, CARGO = @cargo, ATIVO = @ativo WHERE ID = @id";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@nome", obj.nome);
                    cmd.Parameters.AddWithValue("@cpf", obj.cpf);
                    cmd.Parameters.AddWithValue("@telefone", obj.telefone);
                    cmd.Parameters.AddWithValue("@email", obj.email);
                    cmd.Parameters.AddWithValue("@cep", obj.cep);
                    cmd.Parameters.AddWithValue("@endereco", obj.endereco);
                    cmd.Parameters.AddWithValue("@bairro", obj.bairro);
                    cmd.Parameters.AddWithValue("@complemento", obj.complemento);
                    cmd.Parameters.AddWithValue("@cidade", obj.cidade);
                    cmd.Parameters.AddWithValue("@uf", obj.uf);
                    cmd.Parameters.AddWithValue("@dataadmissao", obj.data_admissao);
                    cmd.Parameters.AddWithValue("@cargo", obj.cargo);
                    cmd.Parameters.AddWithValue("@ativo", MainClass.ConvertBoolToChar(obj.ativo));
                    cmd.Parameters.AddWithValue("@id", obj.id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Excluir(int id)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "DELETE FROM FUNCIONARIOS WHERE ID = @id";
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
                string sql = "SELECT f.*, c.NOME FROM FUNCIONARIOS f INNER JOIN CARGOS c ON c.ID = f.CARGO ORDER BY f.NOME ASC";
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

        public DataTable BuscarPorNome(string nome)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "SELECT f.*, c.NOME FROM FUNCIONARIOS f INNER JOIN CARGOS c ON c.ID = f.CARGO WHERE f.NOME LIKE @nome ORDER BY f.NOME ASC";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@nome", "%" + nome + "%");
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

        public DataTable BuscarPorCPF(string cpf)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "SELECT f.*, c.NOME FROM FUNCIONARIOS f INNER JOIN CARGOS c ON c.ID = f.CARGO WHERE f.CPF = @cpf ORDER BY f.NOME ASC";
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

        public DataTable BuscarPorCargo(UInt32 cargo)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "SELECT f.*, c.NOME FROM FUNCIONARIOS f INNER JOIN CARGOS c ON c.ID = f.CARGO WHERE f.CARGO = @cargo ORDER BY f.NOME ASC";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@cargo", cargo);
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

        public Funcionario GetByLogin(string login)
        {
            Funcionario funcionario = new Funcionario();
            using (Conexao con = new Conexao())
            {
                string sql = "SELECT f.*, c.NOME FROM FUNCIONARIOS f LEFT OUTER JOIN CARGOS c ON c.ID = f.CARGO INNER JOIN USUARIOS u ON u.FUNCIONARIO = f.ID WHERE u.LOGIN = @login ORDER BY f.NOME ASC";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@login", login);
                    using (FbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            funcionario.id = Convert.ToInt32(reader["ID"]);
                            funcionario.nome = Convert.ToString(reader["NOME"]);
                            funcionario.cpf = Convert.ToString(reader["CPF"]);
                            funcionario.telefone = Convert.ToString(reader["TELEFONE"]);
                            funcionario.email = Convert.ToString(reader["EMAIL"]);
                            funcionario.cep = Convert.ToString(reader["CEP"]);
                            funcionario.endereco = Convert.ToString(reader["ENDERECO"]);
                            funcionario.bairro = Convert.ToString(reader["BAIRRO"]);
                            funcionario.complemento = Convert.ToString(reader["COMPLEMENTO"]);
                            funcionario.cidade = Convert.ToString(reader["CIDADE"]);
                            funcionario.uf = Convert.ToString(reader["UF"]);
                            funcionario.ativo = MainClass.ConvertCharToBool(Convert.ToChar(reader["ATIVO"]));

                            if (reader["cargo"] != DBNull.Value)
                                funcionario.cargo = Convert.ToInt32(reader["CARGO"]);
                        }
                    }
                }
            }
            return funcionario;
        }

    }
}
