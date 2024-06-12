using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using SmartBoard.Models;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace SmartBoard.Repositories
{
    public class PessoaRepository : DatabaseConnection, IPessoaRepository
    {
        public PessoaRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public void Create(PessoaModel pessoa)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Pessoa (nome, email, senha, tipo) VALUES (@nome, @Email, @Senha, @Tipo)", connection))
            {
                cmd.Parameters.AddWithValue("@Nome", pessoa.Nome);
                cmd.Parameters.AddWithValue("@Email", pessoa.Email);
                cmd.Parameters.AddWithValue("@Senha", pessoa.Senha);
                cmd.Parameters.AddWithValue("@Tipo", pessoa.TipoPessoa);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Pessoa WHERE id_pessoa = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public PessoaModel Login(string email, string senha)
        {
            PessoaModel pessoa = null;


            using (SqlCommand cmd = new SqlCommand("SELECT id_pessoa, nome, email, TipoPessoa, cep, numero, ativo FROM Pessoas WHERE Email = @Login AND senha = @Password", connection))
            {
                cmd.Parameters.Add("@Login", SqlDbType.VarChar).Value = email;
                cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = senha;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        pessoa = new PessoaModel
                        {
                            IdPessoa = (int)reader["id_pessoa"],
                            Email = reader["email"].ToString(),
                            Nome = reader["Nome"].ToString(),
                            TipoPessoa = reader["TipoPessoa"].ToString(),
                            cep = reader["cep"].ToString(),
                            numero = Convert.ToInt16(reader["numero"]),
                            ativo = Convert.ToInt16(reader["ativo"])
                        };
                    }
                }
            }


            return pessoa;
        }

        public IEnumerable<PessoaModel> Read()
        {
            List<PessoaModel> pessoas = new List<PessoaModel>();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Pessoa", connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PessoaModel pessoa = new PessoaModel
                        {
                            IdPessoa = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Email = reader.GetString(2),
                            Senha = reader.GetString(3),
                            TipoPessoa = reader.GetString(4)
                        };
                        pessoas.Add(pessoa);
                    }
                }
            }
            return pessoas;
        }

        public PessoaModel Read(int id)
        {
            PessoaModel pessoa = null;
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Pessoa WHERE id_pessoa = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        pessoa = new PessoaModel
                        {
                            IdPessoa = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Email = reader.GetString(2),
                            Senha = reader.GetString(3),
                            TipoPessoa = reader.GetString(4)
                        };
                    }
                }
            }
            return pessoa;
        }

        public void Update(PessoaModel pessoa)
        {
            using (SqlCommand cmd = new SqlCommand("UPDATE Pessoa SET nome = @Nome, email = @Email, senha = @Senha, tipo = @Tipo WHERE id_pessoa = @Id", connection))
            {
                cmd.Parameters.AddWithValue("@Nome", pessoa.Nome);
                cmd.Parameters.AddWithValue("@Email", pessoa.Email);
                cmd.Parameters.AddWithValue("@Senha", pessoa.Senha);
                cmd.Parameters.AddWithValue("@Tipo", pessoa.TipoPessoa);
                cmd.Parameters.AddWithValue("@Id", pessoa.IdPessoa);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
