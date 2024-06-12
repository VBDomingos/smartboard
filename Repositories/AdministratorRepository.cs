using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using SmartBoard.Models;
using Microsoft.Extensions.Configuration;

namespace SmartBoard.Repositories
{
    public class AdministradorRepository : DatabaseConnection, IAdministradorRepository
    {
        public AdministradorRepository(IConfiguration configuration) : base(configuration) { }

        public void Create(AdministradorModel administrador)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Administrador (id_pessoa) VALUES (@IdPessoa)", connection))
            {
                cmd.Parameters.AddWithValue("@IdPessoa", administrador.IdPessoa);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Administrador WHERE id_administrador = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<AdministradorModel> Read()
        {
            List<AdministradorModel> administradores = new List<AdministradorModel>();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Administrador", connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        AdministradorModel administrador = new AdministradorModel
                        {
                            IdAdministrador = reader.GetInt32(0),
                            IdPessoa = reader.GetInt32(1)
                        };
                        administradores.Add(administrador);
                    }
                }
            }
            return administradores;
        }

        public AdministradorModel Read(int id)
        {
            AdministradorModel administrador = null;
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Administrador WHERE id_administrador = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        administrador = new AdministradorModel
                        {
                            IdAdministrador = reader.GetInt32(0),
                            IdPessoa = reader.GetInt32(1)
                        };
                    }
                }
            }
            return administrador;
        }

        public void Update(AdministradorModel administrador)
        {
            using (SqlCommand cmd = new SqlCommand("UPDATE Administrador SET id_pessoa = @IdPessoa WHERE id_administrador = @Id", connection))
            {
                cmd.Parameters.AddWithValue("@IdPessoa", administrador.IdPessoa);
                cmd.Parameters.AddWithValue("@Id", administrador.IdAdministrador);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
