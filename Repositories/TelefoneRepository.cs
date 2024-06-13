using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using SmartBoard.Models;
using Microsoft.Extensions.Configuration;

namespace SmartBoard.Repositories
{
    public class TelefoneRepository : DatabaseConnection, ITelefoneRepository
    {
        public TelefoneRepository(IConfiguration configuration) : base(configuration) { }

        public void Create(TelefoneModel telefone)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Cliente (id_pessoa) VALUES (@IdPessoa)", connection))
            {
                cmd.Parameters.AddWithValue("@IdPessoa", telefone.IdPessoa);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
