using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using SmartBoard.Models;
using Microsoft.Extensions.Configuration;

namespace SmartBoard.Repositories
{
    public class ClienteRepository : DatabaseConnection, IClienteRepository
    {
        public ClienteRepository(IConfiguration configuration) : base(configuration) { }

        public void Create(ClienteModel cliente)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Cliente (id_pessoa) VALUES (@IdPessoa)", connection))
            {
                cmd.Parameters.AddWithValue("@IdPessoa", cliente.IdPessoa);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Cliente WHERE id_cliente = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<ClienteModel> Read()
        {
            List<ClienteModel> clientes = new List<ClienteModel>();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Cliente", connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ClienteModel cliente = new ClienteModel
                        {
                            IdCliente = reader.GetInt32(0),
                            IdPessoa = reader.GetInt32(1)
                        };
                        clientes.Add(cliente);
                    }
                }
            }
            return clientes;
        }

        public ClienteModel Read(int id)
        {
            ClienteModel cliente = null;
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Cliente WHERE id_cliente = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        cliente = new ClienteModel
                        {
                            IdCliente = reader.GetInt32(0),
                            IdPessoa = reader.GetInt32(1)
                        };
                    }
                }
            }
            return cliente;
        }

        public void Update(ClienteModel cliente)
        {
            using (SqlCommand cmd = new SqlCommand("UPDATE Cliente SET id_pessoa = @IdPessoa WHERE id_cliente = @Id", connection))
            {
                cmd.Parameters.AddWithValue("@IdPessoa", cliente.IdPessoa);
                cmd.Parameters.AddWithValue("@Id", cliente.IdCliente);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
