using FirebirdSql.Data.FirebirdClient;
using Sistema.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.DAO
{
    internal class CategoriaDAO
    {

        private readonly Conexao vcon;

        public CategoriaDAO()
        {
            this.vcon = new Conexao();
        }

        public void Cadastrar(Categoria obj)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "INSERT INTO categorias (descricao, data_cadastro, ativo) VALUES (@descricao, @datacadastro, @ativo)";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@descricao", obj.descricao);
                    cmd.Parameters.AddWithValue("@datacadastro", obj.data_cadastro);
                    cmd.Parameters.AddWithValue("@ativo", obj.ativo);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Editar(Categoria obj)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "UPDATE categorias SET descricao = @descricao, data_cadastro = @datacadastro, ativo = @ativo WHERE id = @id";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@descricao", obj.descricao);
                    cmd.Parameters.AddWithValue("@datacadastro", obj.data_cadastro);
                    cmd.Parameters.AddWithValue("@ativo", obj.ativo);
                    cmd.Parameters.AddWithValue("@id", obj.id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Excluir(int id)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "DELETE FROM categorias WHERE id = @id";
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
                string sql = "SELECT * FROM categorias ORDER BY descricao ASC";
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

        public DataTable BuscarPorDescricao(string descricao)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "SELECT * FROM categorias WHERE descricao LIKE @descricao ORDER BY descricao ASC";
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

    }
}
