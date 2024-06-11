using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using SmartBoard.Models;
using Microsoft.Extensions.Configuration;

namespace SmartBoard.Repositories
{
    public class PessoaRepository : DatabaseConnection, IPessoaRepository
    {
        public PessoaRepository(IConfiguration configuration) : base(configuration) { }

        public void Create(Pessoa pessoa)
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

        public IEnumerable<Pessoa> Read()
        {
            List<Pessoa> pessoas = new List<Pessoa>();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Pessoa", connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Pessoa pessoa = new Pessoa
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

        public Pessoa Read(int id)
        {
            Pessoa pessoa = null;
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Pessoa WHERE id_pessoa = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        pessoa = new Pessoa
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

        public void Update(Pessoa pessoa)
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
