using FirebirdSql.Data.FirebirdClient;
using Sistema.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.DAO
{
    internal class MetodoDAO
    {

        private readonly Conexao vcon;

        public MetodoDAO()
        {
            this.vcon = new Conexao();
        }

        public void Cadastrar(Metodo obj)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "INSERT INTO METODOS (NOME, ATIVO) VALUES (@nome, @ativo)";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@nome", obj.nome);
                    cmd.Parameters.AddWithValue("@ativo", MainClass.ConvertBoolToChar(obj.ativo));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Editar(Metodo obj)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "UPDATE METODOS SET NOME = @nome, ATIVO = @ativo WHERE ID = @id";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@nome", obj.nome);
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
                string sql = "DELETE FROM METODOS WHERE ID = @id";
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
                string sql = "SELECT * FROM METODOS ORDER BY NOME ASC";
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
                string sql = "SELECT * FROM METODOS WHERE NOME LIKE @nome ORDER BY NOME ASC";
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

        public List<Metodo> GetMetodos()
        {
            List<Metodo> metodos = new List<Metodo>();
            using (Conexao con = new Conexao())
            {
                string sql = "SELECT * FROM METODOS ORDER BY NOME ASC";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    using (FbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            metodos.Add(new Metodo
                            {
                                id = Convert.ToInt32(reader["ID"]),
                                nome = Convert.ToString(reader["NOME"]),
                                ativo = MainClass.ConvertCharToBool(Convert.ToChar(reader["ATIVO"]))
                            });
                        }
                    }
                }
            }

            return metodos;
        }

    }
}
