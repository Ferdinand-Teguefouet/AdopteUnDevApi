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
    public class RepositorySkill : IRepository<Skill>
    {
        private readonly string _connectionString;
        public RepositorySkill(IConfiguration configuration)
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
                    cmd.CommandText = "DELETE FROM Skills WHERE Id = @myId";
                    cmd.Parameters.AddWithValue("myId", _id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Skill> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, SkillName, Description FROM Skills";

                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new Skill
                            {
                                Id = (int)reader["Id"],
                                SkillName = reader["SkillName"].ToString(),
                                Description = reader["Description"].ToString()
                            };
                        }
                    }
                }
            }
        }

        public Skill GetById(int _id)
        {
            Skill s = new Skill();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Skills WHERE Id = @myId";
                    cmd.Parameters.AddWithValue("myId", _id);
                    
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            s.Id = (int)reader["Id"];
                            s.SkillName = reader["SkillName"].ToString();
                            s.Description = reader["Description"].ToString();
                        }
                    }
                }
            }
            return s;
        }

        public void Insert(Skill _obj)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Skills (SkillName, Description)" +
                        "VALUES (@sname, @description)";
                    cmd.Parameters.AddWithValue("sname", _obj.SkillName);
                    cmd.Parameters.AddWithValue("description", _obj.Description);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Skill _obj)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Skills SET SkillName = @sname, Description = @description WHERE Id = @myId";

                    cmd.Parameters.AddWithValue("sname", _obj.SkillName);
                    cmd.Parameters.AddWithValue("description", _obj.Description);
                    cmd.Parameters.AddWithValue("myId", _obj.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
