using Data_Access_Layer.Entities;
using Data_Access_Layer.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository
{
    public class RepositoryContract : IRepository<Contract>
    {
        private readonly string _connectionString;
        public RepositoryContract(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public void Delete(int _id)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using(SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Contracts WHERE Id = @myId";
                    cmd.Parameters.AddWithValue("myId", _id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Contract> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Description, Price, DeadLine FROM Contracts";
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new Contract
                            {
                                Id = (int)reader["Id"],
                                Description = reader["Description"].ToString(),
                                Price = (decimal)reader["Price"],
                                DeadLine = (DateTime)reader["DeadLine"]
                            };
                        }
                    }
                }
            }
        }

        public Contract GetById(int _id)
        {
            Contract c = new Contract();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Contracts WHERE Id = @myId";
                    cmd.Parameters.AddWithValue("myId", _id);

                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            c.Id = (int)reader["Id"];
                            c.Description = reader["Description"].ToString();
                            c.Price = (decimal)reader["Price"];
                            c.DeadLine = (DateTime)reader["DeadLine"];
                        }
                    }
                }
            }
            return c;
        }

        public void Insert(Contract _obj)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Contracts (Description, Price, DeadLine)" +
                        "VALUES (@description, @price, @dLine)";
                    cmd.Parameters.AddWithValue("description", _obj.Description);
                    cmd.Parameters.AddWithValue("price", _obj.Price);
                    cmd.Parameters.AddWithValue("dLine", _obj.DeadLine);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Contract _obj)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Contracts SET Description = @description, Price = @price, DeadLine = @dLine WHERE Id = @myId";
                    cmd.Parameters.AddWithValue("myId", _obj.Id);
                    cmd.Parameters.AddWithValue("description", _obj.Description);
                    cmd.Parameters.AddWithValue("price", _obj.Price);
                    cmd.Parameters.AddWithValue("dLine", _obj.DeadLine);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
