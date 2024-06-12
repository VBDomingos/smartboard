using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using SmartBoard.Models;
using Microsoft.Extensions.Configuration;

namespace SmartBoard.Repositories
{
    public class AmbienteRepository : DatabaseConnection, IAmbienteRepository
    {
        public AmbienteRepository(IConfiguration configuration) : base(configuration) { }

        public void Create(AmbienteModel ambiente)
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

        public IEnumerable<AmbienteModel> Read()
        {
            List<AmbienteModel> ambientes = new List<AmbienteModel>();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Ambiente", connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        AmbienteModel ambiente = new AmbienteModel
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

        public AmbienteModel Read(int id)
        {
            AmbienteModel ambiente = null;
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Ambiente WHERE id_ambiente = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ambiente = new AmbienteModel
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

        public void Update(AmbienteModel ambiente)
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
