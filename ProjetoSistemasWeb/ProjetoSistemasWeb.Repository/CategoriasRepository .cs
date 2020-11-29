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
    public class CategoriasRepository : ICategoriasRepository
    {
        private string stringConexao;

        public CategoriasRepository(string stringConexao)
        {
            //stringConexao = "Server=127.0.0.1;Port=5432;Database=myDataBase;User Id=myUsername; Password =Fran";
            this.stringConexao = stringConexao;
        }

        public void Alterar(Categorias categorias)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(this.stringConexao))
            {
                con.Open();
                using (var transacao = con.BeginTransaction())
                {
                    try
                    {
                        NpgsqlCommand comando = new NpgsqlCommand();
                        comando.Connection = con;
                        comando.Transaction = transacao;
                        // Inserir o Produto
                        comando.CommandText = "Update public.categorias " +
                                                " set descricao = @descricao" +
                                                " Where id = @id ";

                        comando.Parameters.AddWithValue("id", categorias.Id);
                        comando.Parameters.AddWithValue("descricao", categorias.Descricao);
                        //Executando comando
                        comando.ExecuteNonQuery();
                        transacao.Commit();
                    }
                    catch (Exception e)
                    {
                        transacao.Rollback();
                        throw e;
                    }
                }
            }
        }

        public void Excluir(Guid id)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(this.stringConexao))
            {
                con.Open();
                using (var transacao = con.BeginTransaction())
                {
                    try
                    {
                        NpgsqlCommand comando = new NpgsqlCommand();
                        comando.Connection = con;
                        comando.Transaction = transacao;
                        // Inserir o Produto
                        comando.CommandText = "Delete from categorias Where id = @id ";
                        comando.Parameters.AddWithValue("id", id);
                        //Executando comando
                        comando.ExecuteNonQuery();
                        transacao.Commit();
                    }
                    catch (Exception e)
                    {
                        transacao.Rollback();
                        throw e;
                    }
                }
            }
        }

        public void Inserir(Categorias categorias)
        {

            using (NpgsqlConnection con = new NpgsqlConnection(this.stringConexao))
            {
                con.Open();

                using (var transacao = con.BeginTransaction())
                {
                    try
                    {
                        NpgsqlCommand comando = new NpgsqlCommand();
                        comando.Connection = con;
                        comando.Transaction = transacao;
                        // Inserir o Produto
                        comando.CommandText = "Insert into categorias " + 
                                                " (id,descricao) " +
                                                " Values (@id,@descricao)";

                        comando.Parameters.AddWithValue("id", categorias.Id);
                        comando.Parameters.AddWithValue("descricao", categorias.Descricao);
                        //Executando comando
                        comando.ExecuteNonQuery();
                        transacao.Commit();
                    }
                    catch (Exception e)
                    {
                        transacao.Rollback();
                        throw e;
                    }
                }
            }
        }

        public Categorias Selecionar(Guid id)
        {
            Categorias cat = null;
            using (NpgsqlConnection con = new NpgsqlConnection(this.stringConexao))
            {
                con.Open();

                NpgsqlCommand comando = new NpgsqlCommand();
                comando.Connection = con;
                // Inserir o Produto
                comando.CommandText = " Select categorias.* from categorias " +
                                        " where categorias.id = @id";

                comando.Parameters.AddWithValue("id", id);
                var leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    cat = new Categorias()
                    {
                        Id = Guid.Parse(leitor["id"].ToString()),
                        Descricao = leitor["descricao"].ToString()
                    };
                };

                return cat;
            }
        }

        public List<Categorias> SelecionarTodos()
        {
            List<Categorias> categorias = new List<Categorias>();
            using (NpgsqlConnection con = new NpgsqlConnection(this.stringConexao))
            {
                con.Open();

                NpgsqlCommand comando = new NpgsqlCommand();
                comando.Connection = con;
                // Inserir o Produto
                comando.CommandText = " Select categorias.* from categorias ";

                var leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    categorias.Add(new Categorias()
                        {
                            Id = Guid.Parse(leitor["id"].ToString()),
                            Descricao = leitor["descricao"].ToString()
                        }
                    );
                };

                return categorias;
            }
        }
    }
}
