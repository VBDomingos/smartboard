using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Dashboard.Models;
using Microsoft.Extensions.Configuration;

namespace Dashboard.Repositories
{
    public class AmbienteRepository : DatabaseConnection, IAmbienteRepository
    {
        public AmbienteRepository(IConfiguration configuration) : base(configuration) { }

        public void Create(Ambiente ambiente)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Ambiente (nome, id_cliente) VALUES (@Nome, @IdCliente)", connection))
            {
                cmd.Parameters.AddWithValue("@Nome", ambiente.Nome);
                cmd.Parameters.AddWithValue("@IdCliente", ambiente.IdCliente);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Ambiente WHERE id_ambiente = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Ambiente> Read()
        {
            List<Ambiente> ambientes = new List<Ambiente>();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Ambiente", connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Ambiente ambiente = new Ambiente
                        {
                            IdAmbiente = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            IdCliente = reader.GetInt32(2)
                        };
                        ambientes.Add(ambiente);
                    }
                }
            }
            return ambientes;
        }

        public Ambiente Read(int id)
        {
            Ambiente ambiente = null;
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Ambiente WHERE id_ambiente = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ambiente = new Ambiente
                        {
                            IdAmbiente = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            IdCliente = reader.GetInt32(2)
                        };
                    }
                }
            }
            return ambiente;
        }

        public void Update(Ambiente ambiente)
        {
            using (SqlCommand cmd = new SqlCommand("UPDATE Ambiente SET nome = @Nome, id_cliente = @IdCliente WHERE id_ambiente = @Id", connection))
            {
                cmd.Parameters.AddWithValue("@Nome", ambiente.Nome);
                cmd.Parameters.AddWithValue("@IdCliente", ambiente.IdCliente);
                cmd.Parameters.AddWithValue("@Id", ambiente.IdAmbiente);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
