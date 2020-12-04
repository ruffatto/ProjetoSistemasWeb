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
            stringConexao = "Server=127.0.0.1;Port=5432;Database=PicBuy;User Id=postgres; Password =123";
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
                                                "preco = @preco, imagem = @imagem, idcategoria = @idcategoria " +
                                                "Where id = @id ";

                        comando.Parameters.AddWithValue("id", produtos.Id);
                        comando.Parameters.AddWithValue("codigo", produtos.Codigo);
                        comando.Parameters.AddWithValue("descricao", produtos.Descricao);
                        comando.Parameters.AddWithValue("acessos", produtos.Acessos);
                        comando.Parameters.AddWithValue("preco", produtos.Preco);
                        comando.Parameters.AddWithValue("imagem", produtos.Imagem);
                        comando.Parameters.AddWithValue("idcategoria", produtos.IdCategoria);
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
                                                " (id,codigo,descricao,acessos,preco,imagem,idcategoria) " +
                                                " Values (@id,@codigo,@descricao,@acessos,@preco,@imagem,@idcategoria)";

                        comando.Parameters.AddWithValue("id", produtos.Id);
                        comando.Parameters.AddWithValue("codigo", produtos.Codigo);
                        comando.Parameters.AddWithValue("descricao", produtos.Descricao);
                        comando.Parameters.AddWithValue("acessos", produtos.Acessos);
                        comando.Parameters.AddWithValue("preco", produtos.Preco);
                        comando.Parameters.AddWithValue("imagem", produtos.Imagem);
                        comando.Parameters.AddWithValue("idcategoria", produtos.IdCategoria);
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
                                        " left outer join categorias on categorias.id = produtos.idcategoria " +
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
                        Preco = (double)leitor["preco"],
                        IdCategoria = Guid.Parse(leitor["id"].ToString())
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
                                        " left outer join categorias on categorias.id = produtos.idcategoria ";

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
                            Preco = (double)leitor["preco"],
                            IdCategoria = Guid.Parse(leitor["id"].ToString())
                    }
                    );
                };

                return produtos;
            }
        }
    }
}
