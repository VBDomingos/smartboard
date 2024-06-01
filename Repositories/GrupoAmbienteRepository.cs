using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Dashboard.Models;
using Microsoft.Extensions.Configuration;

namespace Dashboard.Repositories
{
    public class GrupoAmbienteRepository : DatabaseConnection, IGrupoAmbienteRepository
    {
        public GrupoAmbienteRepository(IConfiguration configuration) : base(configuration) { }

        public void Create(GrupoAmbiente grupoAmbiente)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO GrupoAmbiente (nome, id_cliente) VALUES (@Nome, @IdCliente)", connection))
            {
                cmd.Parameters.AddWithValue("@Nome", grupoAmbiente.Nome);
                cmd.Parameters.AddWithValue("@IdCliente", grupoAmbiente.IdCliente);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM GrupoAmbiente WHERE id_grupo = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<GrupoAmbiente> Read()
        {
            List<GrupoAmbiente> gruposAmbiente = new List<GrupoAmbiente>();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM GrupoAmbiente", connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        GrupoAmbiente grupoAmbiente = new GrupoAmbiente
                        {
                            IdGrupo = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            IdCliente = reader.GetInt32(2)
                        };
                        gruposAmbiente.Add(grupoAmbiente);
                    }
                }
            }
            return gruposAmbiente;
        }

        public GrupoAmbiente Read(int id)
        {
            GrupoAmbiente grupoAmbiente = null;
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM GrupoAmbiente WHERE id_grupo = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        grupoAmbiente = new GrupoAmbiente
                        {
                            IdGrupo = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            IdCliente = reader.GetInt32(2)
                        };
                    }
                }
            }
            return grupoAmbiente;
        }

        public void Update(GrupoAmbiente grupoAmbiente)
        {
            using (SqlCommand cmd = new SqlCommand("UPDATE GrupoAmbiente SET nome = @Nome, id_cliente = @IdCliente WHERE id_grupo = @Id", connection))
            {
                cmd.Parameters.AddWithValue("@Nome", grupoAmbiente.Nome);
                cmd.Parameters.AddWithValue("@IdCliente", grupoAmbiente.IdCliente);
                cmd.Parameters.AddWithValue("@Id", grupoAmbiente.IdGrupo);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
