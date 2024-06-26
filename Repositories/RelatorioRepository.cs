using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using SmartBoard.Models;
using Microsoft.Extensions.Configuration;

namespace SmartBoard.Repositories
{
    public class RelatorioRepository : DatabaseConnection, IRelatorioRepository
    {
        public RelatorioRepository(IConfiguration configuration) : base(configuration) { }

        public void Create(RelatorioModel relatorio)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Relatorio (id_cliente, id_tecnico, id_dispositivo, data_instalacao, descricao) VALUES (@IdCliente, @IdTecnico, @IdDispositivo, @DataInstalacao, @Descricao)", connection))
            {
                cmd.Parameters.AddWithValue("@IdCliente", relatorio.IdCliente);
                cmd.Parameters.AddWithValue("@IdTecnico", relatorio.IdTecnico);
                cmd.Parameters.AddWithValue("@IdDispositivo", relatorio.IdDispositivo);
                cmd.Parameters.AddWithValue("@DataInstalacao", relatorio.DataInstalacao);
                cmd.Parameters.AddWithValue("@Descricao", relatorio.Descricao);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Relatorio WHERE id_relatorio = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<RelatorioModel> Read()
        {
            List<RelatorioModel> relatorios = new List<RelatorioModel>();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Relatorio", connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RelatorioModel relatorio = new RelatorioModel
                        {
                            IdRelatorio = reader.GetInt32(0),
                            IdCliente = reader.GetInt32(1),
                            IdTecnico = reader.GetInt32(2),
                            IdDispositivo = reader.GetInt32(3),
                            DataInstalacao = reader.GetDateTime(4),
                            Descricao = reader.GetString(5)
                        };
                        relatorios.Add(relatorio);
                    }
                }
            }
            return relatorios;
        }

        public RelatorioModel Read(int id)
        {
            RelatorioModel relatorio = null;
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Relatorio WHERE id_relatorio = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        relatorio = new RelatorioModel
                        {
                            IdRelatorio = reader.GetInt32(0),
                            IdCliente = reader.GetInt32(1),
                            IdTecnico = reader.GetInt32(2),
                            IdDispositivo = reader.GetInt32(3),
                            DataInstalacao = reader.GetDateTime(4),
                            Descricao = reader.GetString(5)
                        };
                    }
                }
            }
            return relatorio;
        }

        public void Update(RelatorioModel relatorio)
        {
            throw new NotImplementedException();
        }

        //update todo
    }
};
