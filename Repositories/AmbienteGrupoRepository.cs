using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using SmartBoard.Models;
using Microsoft.Extensions.Configuration;

namespace SmartBoard.Repositories
{
    public class AmbienteGrupoRepository : DatabaseConnection, IAmbienteGrupoRepository
    {
        public AmbienteGrupoRepository(IConfiguration configuration) : base(configuration) { }

        public void Create(AmbienteGrupo ambienteGrupo)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO AmbienteGrupo (id_grupo, id_ambiente) VALUES (@IdGrupo, @IdAmbiente)", connection))
            {
                cmd.Parameters.AddWithValue("@IdGrupo", ambienteGrupo.IdGrupo);
                cmd.Parameters.AddWithValue("@IdAmbiente", ambienteGrupo.IdAmbiente);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int idGrupo, int idAmbiente)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM AmbienteGrupo WHERE id_grupo = @IdGrupo AND id_ambiente = @IdAmbiente", connection))
            {
                cmd.Parameters.AddWithValue("@IdGrupo", idGrupo);
                cmd.Parameters.AddWithValue("@IdAmbiente", idAmbiente);
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<AmbienteGrupo> Read()
        {
            List<AmbienteGrupo> ambientesGrupos = new List<AmbienteGrupo>();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM AmbienteGrupo", connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        AmbienteGrupo ambienteGrupo = new AmbienteGrupo
                        {
                            IdGrupo = reader.GetInt32(0),
                            IdAmbiente = reader.GetInt32(1)
                        };
                        ambientesGrupos.Add(ambienteGrupo);
                    }
                }
            }
            return ambientesGrupos;
        }
    }
}