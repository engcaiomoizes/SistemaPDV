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
    internal class CargoDAO
    {

        private readonly Conexao vcon;

        public CargoDAO()
        {
            this.vcon = new Conexao();
        }

        public void Cadastrar(Cargo obj)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "INSERT INTO cargos (nome) VALUES (@nome)";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@nome", obj.nome);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Editar(Cargo obj)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "UPDATE cargos SET nome = @nome WHERE id = @id";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@nome", obj.nome);
                    cmd.Parameters.AddWithValue("@id", obj.id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Excluir(int id)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "DELETE FROM cargos WHERE id = @id";
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
                string sql = "SELECT * FROM cargos ORDER BY nome ASC";
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
                string sql = "SELECT * FROM cargos WHERE nome LIKE @nome ORDER BY nome ASC";
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

    }
}
