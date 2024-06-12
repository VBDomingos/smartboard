using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using SmartBoard.Models;
using Microsoft.Extensions.Configuration;

namespace SmartBoard.Repositories
{
    public class DispositivoRepository : DatabaseConnection, IDispositivoRepository
    {
        public DispositivoRepository(IConfiguration configuration) : base(configuration) { }

        public void Create(DispositivoModel dispositivo)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Dispositivo (nome, id_ambiente) VALUES (@Nome, @IdAmbiente)", connection))
            {
                cmd.Parameters.AddWithValue("@Nome", dispositivo.Nome);
                cmd.Parameters.AddWithValue("@IdAmbiente", dispositivo.IdAmbiente);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Dispositivo WHERE id_dispositivo = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<DispositivoModel> Read()
        {
            List<DispositivoModel> dispositivos = new List<DispositivoModel>();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Dispositivo", connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DispositivoModel dispositivo = new DispositivoModel
                        {
                            IdDispositivo = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            IdAmbiente = reader.GetInt32(2)
                        };
                        dispositivos.Add(dispositivo);
                    }
                }
            }
            return dispositivos;
        }

        public DispositivoModel Read(int id)
        {
            DispositivoModel dispositivo = null;
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Dispositivo WHERE id_dispositivo = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        dispositivo = new DispositivoModel
                        {
                            IdDispositivo = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            IdAmbiente = reader.GetInt32(2)
                        };
                    }
                }
            }
            return dispositivo;
        }

        public void Update(DispositivoModel dispositivo)
        {
            using (SqlCommand cmd = new SqlCommand("UPDATE Dispositivo SET nome = @Nome, id_ambiente = @IdAmbiente WHERE id_dispositivo = @Id", connection))
            {
                cmd.Parameters.AddWithValue("@Nome", dispositivo.Nome);
                cmd.Parameters.AddWithValue("@IdAmbiente", dispositivo.IdAmbiente);
                cmd.Parameters.AddWithValue("@Id", dispositivo.IdDispositivo);
                cmd.ExecuteNonQuery();
            }
        }
    }
}