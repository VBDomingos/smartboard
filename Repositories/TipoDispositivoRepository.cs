using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using SmartBoard.Models;
using Microsoft.Extensions.Configuration;

namespace SmartBoard.Repositories
{
    public class TipoDispositivoRepository : DatabaseConnection, ITipoDispositivoRepository
    {
        public TipoDispositivoRepository(IConfiguration configuration) : base(configuration) { }

        public void Create(TelefoneModel telefone)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Cliente (id_pessoa) VALUES (@IdPessoa)", connection))
            {
                cmd.Parameters.AddWithValue("@IdPessoa", telefone.IdPessoa);

                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<TipoDispositivoModel> GetAllTipoDispositivo()
        {
            List<TipoDispositivoModel> tipoDispositivo = new List<TipoDispositivoModel>();

            using (SqlCommand cmd = new SqlCommand("SELECT id_tipodispositivo, nometipodispositivo FROM tipodispositivos", connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var tipoDispositivoAux = new TipoDispositivoModel
                        {
                            IdTipoDispositivo = (int)reader["id_ambiente"],
                            Nome = reader["nome"].ToString(),
                        };
                        tipoDispositivo.Add(tipoDispositivoAux);
                    }
                }
            }
            return tipoDispositivo;
        }
    }
}
