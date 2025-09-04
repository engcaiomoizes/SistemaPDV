using FirebirdSql.Data.FirebirdClient;
using Sistema.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.DAO
{
    internal class UsuarioDAO
    {
        
        private readonly Conexao vcon;

        public UsuarioDAO()
        {
            this.vcon = new Conexao();
        }

        public void CadastrarUsuario(Usuario obj)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "INSERT INTO USUARIOS (LOGIN, SENHA, FUNCIONARIO, ATIVO) VALUES (@usuario, @senha, @funcionario, @ativo)";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@funcionario", obj.funcionario);
                    cmd.Parameters.AddWithValue("@usuario", obj.login);
                    cmd.Parameters.AddWithValue("@senha", obj.senha);
                    cmd.Parameters.AddWithValue("@ativo", MainClass.ConvertBoolToChar(obj.ativo));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EditarUsuario(Usuario obj)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "UPDATE USUARIOS SET LOGIN = @usuario, SENHA = @senha, FUNCIONARIO = @funcionario, ATIVO = @ativo WHERE ID = @id";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@funcionario", obj.funcionario);
                    cmd.Parameters.AddWithValue("@usuario", obj.login);
                    cmd.Parameters.AddWithValue("@senha", obj.senha);
                    cmd.Parameters.AddWithValue("@ativo", MainClass.ConvertBoolToChar(obj.ativo));
                    cmd.Parameters.AddWithValue("@id", obj.id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ExcluirUsuario(int id)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "DELETE FROM USUARIOS WHERE ID = @id";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable ListarUsuarios()
        {
            using (Conexao con = new Conexao())
            {
                string sql = "SELECT u.ID, u.LOGIN, u.SENHA, f.NOME, u.ATIVO, u.FUNCIONARIO FROM USUARIOS u INNER JOIN FUNCIONARIOS f ON f.ID = u.FUNCIONARIO ORDER BY u.LOGIN ASC";
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

        public DataTable BuscarPorLogin(string login)
        {
            using (Conexao con = new Conexao())
            {
                string sql = "SELECT u.ID, u.LOGIN, u.SENHA, f.NOME, u.ATIVO, u.FUNCIONARIO FROM USUARIOS u INNER JOIN FUNCIONARIOS f ON f.ID = u.FUNCIONARIO WHERE u.LOGIN LIKE @login ORDER BY u.LOGIN ASC";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@login", "%" + login + "%");
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
