using Npgsql;
using ProjetoSistemasWeb.Domain.Entities;
using ProjetoSistemasWeb.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoSistemasWeb.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private string connectionString;

        public UsuarioRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool Delete(Guid id)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                //Inicia a transação
                using (var trans = con.BeginTransaction())
                {
                    try
                    {
                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.Connection = con;
                        cmd.Transaction = trans;
                        cmd.CommandText = @"DELETE FROM usuarios WHERE Id=@Id";
                        cmd.Parameters.AddWithValue("Id", id);
                        cmd.ExecuteNonQuery();
                        //commit na transação
                        trans.Commit();
                        return true;

                    }
                    catch (Exception ex)
                    {
                        //rollback da transação
                        trans.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public Usuario Find(Guid id)
        {
            Usuario usuario = null;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT * FROM Usuarios WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", id);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    usuario = new Usuario();
                    usuario.Id = Guid.Parse(reader["id"].ToString());
                    usuario.Password = reader["password"].ToString();
                    usuario.UserName = reader["username"].ToString();
                }
                reader.Close();

                return usuario;
            }
        }

        public Usuario Find(string email)
        {
            Usuario usuario = null;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT * FROM Usuarios WHERE username=@username";
                cmd.Parameters.AddWithValue("username", email);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    usuario = new Usuario();
                    usuario.Id = Guid.Parse(reader["id"].ToString());
                    usuario.Password = reader["password"].ToString();
                    usuario.UserName = reader["username"].ToString();
                }
                reader.Close();

                return usuario;
            }
        }

        public List<Usuario> FindAll()
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT * FROM Usuarios";
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = Guid.Parse(reader["id"].ToString());
                    usuario.Password = reader["password"].ToString();
                    usuario.UserName = reader["username"].ToString();

                    usuarios.Add(usuario);
                }
                return usuarios;
            }

        }

        public Guid Insert(Usuario usuario)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                //Inicia a transação
                using (var trans = con.BeginTransaction())
                {
                    try
                    {
                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.Connection = con;
                        cmd.Transaction = trans;
                        cmd.CommandText = @"INSERT Into Usuarios (id, username, password) values(@id, @username, @password)";
                        cmd.Parameters.AddWithValue("id", usuario.Id);
                        cmd.Parameters.AddWithValue("username", usuario.UserName);
                        cmd.Parameters.AddWithValue("password", usuario.Password);
                        cmd.ExecuteNonQuery();

                        //commit na transação
                        trans.Commit();
                        return usuario.Id;

                    }
                    catch (Exception ex)
                    {
                        //rollback da transação
                        trans.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public Guid Update(Usuario usuario)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                //Inicia a transação
                using (var trans = con.BeginTransaction())
                {
                    try
                    {
                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.Connection = con;
                        cmd.Transaction = trans;
                        cmd.CommandText = @"UPDATE Usuarios SET username=@username, password=@password WHERE Id=@id";
                        cmd.Parameters.AddWithValue("id", usuario.Id);
                        cmd.Parameters.AddWithValue("username", usuario.UserName);
                        cmd.Parameters.AddWithValue("password", usuario.Password);
                        cmd.ExecuteNonQuery();

                        //commit na transação
                        trans.Commit();
                        return usuario.Id;

                    }
                    catch (Exception ex)
                    {
                        //rollback da transação
                        trans.Rollback();
                        throw ex;
                    }
                }
            }
        }
    }
}
