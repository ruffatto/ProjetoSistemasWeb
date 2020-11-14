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
    public class ProdutosRepository : IProdutosRepository
    {
        private string stringConexao;

        public ProdutosRepository(string stringConexao)
        {
            //stringConexao = "Server=127.0.0.1;Port=5432;Database=myDataBase;User Id=myUsername; Password =Fran";
            this.stringConexao = stringConexao;
        }

        public void Alterar(Produtos produtos)
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
                        comando.CommandText = "Update public.produtos " +
                                                " set codigo = @codigo, descricao = @descricao, acessos = @acessos," +
                                                "preco = @preco, imagem = @imagem " +
                                                "Where id = @id ";

                        comando.Parameters.AddWithValue("id", produtos.Id);
                        comando.Parameters.AddWithValue("codigo", produtos.Codigo);
                        comando.Parameters.AddWithValue("descricao", produtos.Descricao);
                        comando.Parameters.AddWithValue("acessos", produtos.Acessos);
                        comando.Parameters.AddWithValue("preco", produtos.Preco);
                        comando.Parameters.AddWithValue("imagem", produtos.Imagem);
                        //Executando comando
                        comando.ExecuteNonQuery();

                        //Inserir a Categoria
                        comando.CommandText = "Update public.categorias " +
                                                " set descricao = @descricao " +
                                                " Where produtoid = @id ";

                        comando.Parameters.AddWithValue("id", produtos.Categoria.Id);
                        comando.Parameters.AddWithValue("produtoid", produtos.Id);
                        comando.Parameters.AddWithValue("descricao", produtos.Categoria.Descricao);
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
                        comando.CommandText = "Delete from produtos Where id = @id ";
                        comando.Parameters.AddWithValue("id", id);
                        //Executando comando
                        comando.ExecuteNonQuery();

                        //Inserir a Categoria
                        comando.CommandText = "Delete from categorias Where produtosid = @id ";
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

        public void Inserir(Produtos produtos)
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
                        comando.CommandText = "Insert into produtos " + 
                                                " (id,codigo,descricao,acessos,preco,imagem) " +
                                                " Values (@id,@codigo,@descricao,@acessos,@preco,@imagem)";

                        comando.Parameters.AddWithValue("id", produtos.Id);
                        comando.Parameters.AddWithValue("codigo", produtos.Codigo);
                        comando.Parameters.AddWithValue("descricao", produtos.Descricao);
                        comando.Parameters.AddWithValue("acessos", produtos.Acessos);
                        comando.Parameters.AddWithValue("preco", produtos.Preco);
                        comando.Parameters.AddWithValue("imagem", produtos.Imagem);
                        //Executando comando
                        comando.ExecuteNonQuery();

                        //Inserir a Categoria
                        comando.CommandText = "Insert into categorias " +
                                                " (id,produtoid,descricao) " +
                                                " Values (@id,@descricao)";

                        comando.Parameters.AddWithValue("id", produtos.Categoria.Id);
                        comando.Parameters.AddWithValue("produtoid", produtos.Id);
                        comando.Parameters.AddWithValue("descricao", produtos.Categoria.Descricao);
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

        public Produtos Selecionar(Guid id)
        {
            Produtos prod = null;
            using (NpgsqlConnection con = new NpgsqlConnection(this.stringConexao))
            {
                con.Open();

                NpgsqlCommand comando = new NpgsqlCommand();
                comando.Connection = con;
                // Inserir o Produto
                comando.CommandText = " Select produtos.*, categorias.* from produtos " +
                                        " left outer join categorias on categorias.produtoid = produtos.id " +
                                        " where produtos.id = @id" ;

                comando.Parameters.AddWithValue("id", id);
                var leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    prod = new Produtos()
                    {
                        Id = Guid.Parse(leitor["id"].ToString()),
                        Descricao = leitor["descricao"].ToString(),
                        Codigo = Convert.ToInt32(leitor["codigo"]),
                        Imagem = leitor["imagem"].ToString(),
                        Acessos = Convert.ToInt32(leitor["acessos"]),
                        Preco = (float)leitor["preco"],
                        Categoria = new Categorias()
                        {
                            Id = Guid.Parse(leitor["id"].ToString()),
                            Descricao = leitor["descricao"].ToString()
                        }
                    };
                };

                return prod;
            }
        }

        public List<Produtos> SelecionarTodos()
        {
            List<Produtos> produtos = new List<Produtos>();
            using (NpgsqlConnection con = new NpgsqlConnection(this.stringConexao))
            {
                con.Open();

                NpgsqlCommand comando = new NpgsqlCommand();
                comando.Connection = con;
                // Inserir o Produto
                comando.CommandText = " Select produtos.*, categorias.* from produtos " +
                                        " left outer join categorias on categorias.produtoid = produtos.id ";

                var leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    produtos.Add(new Produtos()
                        {
                            Id = Guid.Parse(leitor["id"].ToString()),
                            Descricao = leitor["descricao"].ToString(),
                            Codigo = Convert.ToInt32(leitor["codigo"]),
                            Imagem = leitor["imagem"].ToString(),
                            Acessos = Convert.ToInt32(leitor["acessos"]),
                            Preco = (float)leitor["preco"],
                            Categoria = new Categorias()
                            {
                                Id = Guid.Parse(leitor["id"].ToString()),
                                Descricao = leitor["descricao"].ToString()
                            }
                        }
                    );
                };

                return produtos;
            }
        }
    }
}
