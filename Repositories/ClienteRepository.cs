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

        public void Create(Cliente cliente)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Cliente (id_pessoa, endereco, telefone) VALUES (@IdPessoa, @Endereco, @Telefone)", connection))
            {
                cmd.Parameters.AddWithValue("@IdPessoa", cliente.IdPessoa);
                cmd.Parameters.AddWithValue("@Endereco", cliente.Endereco);
                cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);

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

        public IEnumerable<Cliente> Read()
        {
            List<Cliente> clientes = new List<Cliente>();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Cliente", connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Cliente cliente = new Cliente
                        {
                            IdCliente = reader.GetInt32(0),
                            IdPessoa = reader.GetInt32(1),
                            Endereco = reader.GetString(2),
                            Telefone = reader.GetString(3)
                        };
                        clientes.Add(cliente);
                    }
                }
            }
            return clientes;
        }

        public Cliente Read(int id)
        {
            Cliente cliente = null;
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Cliente WHERE id_cliente = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        cliente = new Cliente
                        {
                            IdCliente = reader.GetInt32(0),
                            IdPessoa = reader.GetInt32(1),
                            Endereco = reader.GetString(2),
                            Telefone = reader.GetString(3)
                        };
                    }
                }
            }
            return cliente;
        }

        public void Update(Cliente cliente)
        {
            using (SqlCommand cmd = new SqlCommand("UPDATE Cliente SET id_pessoa = @IdPessoa, endereco = @Endereco, telefone = @Telefone WHERE id_cliente = @Id", connection))
            {
                cmd.Parameters.AddWithValue("@IdPessoa", cliente.IdPessoa);
                cmd.Parameters.AddWithValue("@Endereco", cliente.Endereco);
                cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                cmd.Parameters.AddWithValue("@Id", cliente.IdCliente);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
