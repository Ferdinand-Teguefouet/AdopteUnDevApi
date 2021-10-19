using Data_Access_Layer.Entities;
using Data_Access_Layer.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository
{
    public class RepositoryExistedUser : ILogin
    {
        private readonly string _connectionString;
        public RepositoryExistedUser(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public UserConnected GetExistedUser(UserLogin _uLogin)
        {
            UserConnected ul = new UserConnected();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                SqlCommand cmd = new SqlCommand("UserLogin", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", _uLogin.Email);
                cmd.Parameters.AddWithValue("@Password", _uLogin.Password);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ul.Id = (int)reader["Id"];
                    ul.Email = reader["Email"].ToString();
                    ul.Name = reader["Name"].ToString();
                    ul.Telephone = reader["Name"].ToString();
                    ul.IsClient = (bool)reader["IsClient"];
                }
            }
            return ul;
        }

        public IEnumerable<UserConnected> GetAll()
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
                            yield return new UserConnected
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

        public UserConnected GetById(int _id)
        {
            UserConnected u = new UserConnected();
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
    }
}
