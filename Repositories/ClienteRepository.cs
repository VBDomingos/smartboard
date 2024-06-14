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

        public Tuple<List<ClienteModel>, List<PessoaModel>, List<TelefoneModel>> GetAllClienteInfo()
        {
            List<PessoaModel> pessoa = new List<PessoaModel>();
            List<TelefoneModel> telefone = new List<TelefoneModel>();
            List<ClienteModel> cliente = new List<ClienteModel>();

            using (SqlCommand cmd = new SqlCommand("WITH TelefonePrincipal AS (SELECT t.id_pessoa, t.numero,ROW_NUMBER() OVER (PARTITION BY t.id_pessoa ORDER BY t.id_pessoa ) AS rn FROM telefones t) " +
                "SELECT C.id_cliente, P.id_pessoa, P.nome, P.email, p.cpf, p.cep, p.numero, p.ativo, p.tipopessoa, t.numero FROM Clientes C " +
                "inner join Pessoas P on p.id_pessoa = C.id_pessoa " +
                "LEFT JOIN TelefonePrincipal t ON p.id_pessoa = t.id_pessoa AND t.rn = 1", connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var clienteAux = new ClienteModel
                        {
                            IdCliente = (int)reader["id_cliente"],
                            IdPessoa = (int)reader["id_pessoa"]
                        };
                        cliente.Add(clienteAux);

                        var pessoaAux = new PessoaModel
                        {
                            IdPessoa = (int)reader["id_pessoa"],
                            Email = reader["email"].ToString(),
                            Nome = reader["nome"].ToString(),
                            Cpf = reader["cpf"].ToString(),
                            Cep = reader["cep"].ToString(),
                            Numero = (int)(reader["numero"]),
                            Ativo = Convert.ToInt16(reader["ativo"]),
                            TipoPessoa = Convert.ToChar(reader["tipopessoa"])
                        };
                        pessoa.Add(pessoaAux);

                        var telefoneAux = new TelefoneModel
                        {
                            IdTelefone = (int)reader["id_pessoa"],
                            IdPessoa = (int)reader["id_pessoa"],
                            Celular = (int)reader["numero"]
                        };

                        telefone.Add(telefoneAux);
                    }
                }
            }

            return Tuple.Create(cliente, pessoa, telefone);
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
