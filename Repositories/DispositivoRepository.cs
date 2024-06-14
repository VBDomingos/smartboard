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

        public Tuple<ClienteModel, List<DispositivoModel>, List<AmbienteModel>, List<TipoDispositivoModel>> GetAllDispositivosByCliente(int id)
        {
            ClienteModel cliente = new ClienteModel();
            List<DispositivoModel> dispositivo = new List<DispositivoModel>();
            List<AmbienteModel> ambiente = new List<AmbienteModel>();
            List<TipoDispositivoModel> tipoDispositivo = new List<TipoDispositivoModel>();
            using (SqlCommand cmd = new SqlCommand("SELECT d.id_dispositivo, d.nomedispositivo, d.porta, c.id_cliente, t.nometipodispositivo, t.id_tipodispositivo, a.nomeambiente from Dispositivos d, Ambientes a, Clientes c, tipoDispositivos t " +
                "where d.id_cliente = c.id_cliente and d.id_ambiente = a.id_ambiente and d.id_tipodispositivo = t.id_tipodispositivo and id_dispositivo = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        while (reader.Read())
                        {
                            var dispositivoAux = new DispositivoModel
                            {
                                IdDispositivo = (int)reader["id_dispositivo"],
                                Nome = reader["nomedispositivo"].ToString(),
                                Porta = (int)reader["porta"],
                                IdCliente = (int)reader["id_cliente"],
                                IdTecnico = (int)reader["id_tecnico"],
                                IdTipoDispositivo = (int)reader["id_tipodispositivo"],
                                IdAmbiente = reader.GetInt32(2)
                            };
                            dispositivo.Add(dispositivoAux);

                            var clienteAux = new ClienteModel
                            {
                                IdCliente = (int)reader["id_cliente"]
                                
                            };
                            dispositivo.Add(dispositivoAux);

                            var ambienteAux = new AmbienteModel
                            {
                                Nome = reader["nomeambiente"].ToString()
                            };
                            ambiente.Add(ambienteAux);

                            var tipoDispositivoAux = new TipoDispositivoModel
                            {
                                Nome = reader["tipoDispositivos"].ToString()
                            };
                            tipoDispositivo.Add(tipoDispositivoAux);


                        }
                    }
                }
            }
            return Tuple.Create(cliente, dispositivo,ambiente, tipoDispositivo);
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