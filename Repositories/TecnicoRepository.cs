using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using SmartBoard.Models;
using Microsoft.Extensions.Configuration;

namespace SmartBoard.Repositories
{
    public class TecnicoRepository : DatabaseConnection, ITecnicoRepository
    {
        public TecnicoRepository(IConfiguration configuration) : base(configuration) { }

        public void Create(TecnicoModel tecnico)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Tecnico (id_pessoa, empresa) VALUES (@IdPessoa, @Empresa)", connection))
            {
                cmd.Parameters.AddWithValue("@IdPessoa", tecnico.IdPessoa);
                cmd.Parameters.AddWithValue("@Empresa", tecnico.Empresa);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Tecnico WHERE id_tecnico = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<TecnicoModel> Read()
        {
            List<TecnicoModel> tecnicos = new List<TecnicoModel>();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Tecnico", connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TecnicoModel tecnico = new TecnicoModel
                        {
                            IdTecnico = reader.GetInt32(0),
                            IdPessoa = reader.GetInt32(1),
                            Empresa = reader.GetString(2)
                        };
                        tecnicos.Add(tecnico);
                    }
                }
            }
            return tecnicos;
        }

        public TecnicoModel Read(int id)
        {
            TecnicoModel tecnico = null;
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Tecnico WHERE id_tecnico = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        tecnico = new TecnicoModel
                        {
                            IdTecnico = reader.GetInt32(0),
                            IdPessoa = reader.GetInt32(1),
                            Empresa = reader.GetString(2)
                        };
                    }
                }
            }
            return tecnico;
        }

        public void Update(TecnicoModel tecnico)
        {
            using (SqlCommand cmd = new SqlCommand("UPDATE Tecnico SET id_pessoa = @IdPessoa, empresa = @Empresa WHERE id_tecnico = @Id", connection))
            {
                cmd.Parameters.AddWithValue("@IdPessoa", tecnico.IdPessoa);
                cmd.Parameters.AddWithValue("@Empresa", tecnico.Empresa);
                cmd.Parameters.AddWithValue("@Id", tecnico.IdTecnico);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
