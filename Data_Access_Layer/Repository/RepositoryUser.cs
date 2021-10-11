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
    public class RepositoryUser : IRepository<User>
    {
        private readonly string _connectionString;
        public RepositoryUser(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public void Delete(int _id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Users WHERE Id = @myId";
                    cmd.Parameters.AddWithValue("myId", _id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<User> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Users";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new User
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Email = reader["Email"].ToString(),
                                Telephone = reader["Telephone"].ToString(),
                                IsClient = (bool)reader["IsClient"]
                            };
                        }
                    }
                }
            }
        }

        public User GetById(int _id)
        {
            User u = new User();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Users WHERE Id = @myId";
                    cmd.Parameters.AddWithValue("myId", _id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            u.Id = (int)reader["Id"];
                            u.Name = reader["Name"].ToString();
                            u.Email = reader["Email"].ToString();
                            u.Telephone = reader["Telephone"].ToString();
                            u.IsClient = (bool)reader["IsClient"];
                        }
                    }
                }
            }
            return u;
        }

        public void Insert(User _obj)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Users (Email, Password, Telephone, IsClient, Name)" +
                        "VALUES (@email, @pw, @tel, @isclient, @name)";
                    cmd.Parameters.AddWithValue("name", _obj.Name);
                    cmd.Parameters.AddWithValue("email", _obj.Email);
                    cmd.Parameters.AddWithValue("pw", _obj.Password);
                    cmd.Parameters.AddWithValue("tel", _obj.Telephone);
                    cmd.Parameters.AddWithValue("isclient", _obj.IsClient);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(User _obj)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Users SET Name = @name, Email = @email, Telephone = @tel, " +
                        "IsClient= @isclient WHERE Id = @myId";

                    cmd.Parameters.AddWithValue("name", _obj.Name);
                    cmd.Parameters.AddWithValue("email", _obj.Email);
                    cmd.Parameters.AddWithValue("tel", _obj.Telephone);
                    cmd.Parameters.AddWithValue("isclient", _obj.IsClient);
                    cmd.Parameters.AddWithValue("myId", _obj.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
