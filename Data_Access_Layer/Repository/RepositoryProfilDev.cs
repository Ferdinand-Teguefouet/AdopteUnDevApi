using Data_Access_Layer.Entities.Views;
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
    public class RepositoryProfilDev : IRepository<ProfilDev>
    {
        private readonly string _connectionString;
        public RepositoryProfilDev(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void Delete(int _id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProfilDev> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM ProfilDEV";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new ProfilDev
                            {
                                Name = reader["Name"].ToString(),
                                Skill = reader["SkillName"].ToString(),
                                Category = reader["CategoryName"].ToString(),
                                Description = reader["Description"].ToString()
                            };
                        }
                    }
                }
            }

        }

        public ProfilDev GetById(int _id)
        {
            throw new NotImplementedException();
        }

        public void Insert(ProfilDev _obj)
        {
            throw new NotImplementedException();
        }

        public void Update(ProfilDev _obj)
        {
            throw new NotImplementedException();
        }
    }
}
