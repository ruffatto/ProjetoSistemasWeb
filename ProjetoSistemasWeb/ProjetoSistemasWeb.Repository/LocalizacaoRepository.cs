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
    public class LocalizacaoRepository : ILocalizacaoRepository
    {
        private string stringConexao;

        public LocalizacaoRepository(string stringConexao)
        {
            //stringConexao = "Server=127.0.0.1;Port=5432;Database=myDataBase;User Id=myUsername; Password =Fran";
            this.stringConexao = stringConexao;
        }

        public void Inserir(Localizacao localizacao)
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
                        comando.CommandText = "Insert into localizacao " + 
                                                " (CodigoProduto,data,lat,long) " +
                                                " Values (@CodigoProduto,@data,@lat,@long)";

                        comando.Parameters.AddWithValue("CodigoProduto", localizacao.CodigoProduto);
                        comando.Parameters.AddWithValue("data", localizacao.Data);
                        comando.Parameters.AddWithValue("lat", localizacao.Lat);
                        comando.Parameters.AddWithValue("long", localizacao.Long);
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

        public List<Localizacao> Selecionar(Guid codigoProduto)
        {
            List<Localizacao> localizacao = new List<Localizacao>();
            using (NpgsqlConnection con = new NpgsqlConnection(this.stringConexao))
            {
                con.Open();

                NpgsqlCommand comando = new NpgsqlCommand();
                comando.Connection = con;
                // Inserir o Produto
                comando.CommandText = " Select localizacao.* from localizacao " +
                                    " where localizacao.CodigoProduto = @CodigoProduto";

                comando.Parameters.AddWithValue("CodigoProduto", codigoProduto);
                var leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    localizacao.Add(new Localizacao()
                    {
                        CodigoProduto = Guid.Parse(leitor["CodigoProduto"].ToString()),
                        Data = Convert.ToDateTime(leitor["data"]),
                        Lat = (double)leitor["lat"],
                        Long = (double)leitor["long"]
                    }
                    );
                };

                return localizacao;
            }
        }

        public List<Localizacao> SelecionarTodos()
        {
            List<Localizacao> localizacao = new List<Localizacao>();
            using (NpgsqlConnection con = new NpgsqlConnection(this.stringConexao))
            {
                con.Open();

                NpgsqlCommand comando = new NpgsqlCommand();
                comando.Connection = con;
                // Inserir o Produto
                comando.CommandText = " Select localizacao.* from localizacao ";

                var leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    localizacao.Add(new Localizacao()
                        {
                            CodigoProduto = Guid.Parse(leitor["CodigoProduto"].ToString()),
                            Data = Convert.ToDateTime(leitor["data"]),
                            Lat = (double)leitor["lat"],
                            Long = (double)leitor["long"]
                        }
                    );
                };

                return localizacao;
            }
        }

        public int SelecionarAcessadosDia()
        {
            int Total = 0;
            using (NpgsqlConnection con = new NpgsqlConnection(this.stringConexao))
            {
                con.Open();

                NpgsqlCommand comando = new NpgsqlCommand();
                comando.Connection = con;
                // Inserir o Produto
                comando.CommandText = " Select count(*) as total from localizacao " +
                                        " where data = @date ";

                comando.Parameters.AddWithValue("date", DateTime.Now);
                var leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    Total = (int)leitor["total"];
                };

                return Total;
            }
        }
    }
}
