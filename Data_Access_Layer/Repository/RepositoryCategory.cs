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
    public class RepositoryCategory : IRepository<Category>
    {
        private readonly string _connectionString;

        public RepositoryCategory(IConfiguration configuration)
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
                    cmd.CommandText = "DELETE FROM Categories WHERE Id = @MyId";
                    cmd.Parameters.AddWithValue("MyId", _id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Category> GetAll()
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using(SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT ° FROM Category";
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new Category
                            {
                                Id = (int)reader["Id"],
                                CategoryName = reader["CategoryName"].ToString()
                            };
                        }
                    }
                }
            }
        }

        public Category GetById(int _id)
        {
            Category cat = new Category();
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using(SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Categories WHERE Id = @myId";
                    cmd.Parameters.AddWithValue("myId", _id);

                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cat.Id = (int)reader["Id"];
                            cat.CategoryName = reader["CategoryName"].ToString();
                        }
                    }
                }
            }
            return cat;
        }

        public void Insert(Category _obj)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using(SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "Insert INTO Categories (CategoryName) VALUES (@cName)";
                    cmd.Parameters.AddWithValue("cName", _obj.CategoryName);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Category _obj)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using(SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Categories SET CategoryName = @cateName WHERE Id = @myId";
                    cmd.Parameters.AddWithValue("catName", _obj.CategoryName);
                    cmd.Parameters.AddWithValue("myId", _obj.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
