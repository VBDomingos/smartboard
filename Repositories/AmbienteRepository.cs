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
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<AmbienteModel> GetAllAmbientes()
        {
            List<AmbienteModel> ambiente = new List<AmbienteModel>();

            using (SqlCommand cmd = new SqlCommand("SELECT id_ambiente, nomeambiente FROM Ambientes", connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        while (reader.Read())
                        {
                            var ambienteAux = new AmbienteModel
                            {
                                IdAmbiente = (int)reader["id_ambiente"],
                                Nome = reader["nomeambiente"].ToString(),
                            };
                            ambiente.Add(ambienteAux);
                        }
                    }
                }
            }
            return ambiente;
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
                        };
                    }
                }
            }
            return ambiente;
        }
    }
}
