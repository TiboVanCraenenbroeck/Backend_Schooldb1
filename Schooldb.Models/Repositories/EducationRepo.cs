/*using Microsoft.Extensions.Configuration;
using Schooldb.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Schooldb.Repositories
{
    public class EducationRepo : IEducationRepo
    {
        private readonly string connectionString;

        public EducationRepo(IConfiguration configuration)
        {
            connectionString = ConfigurationExtensions.GetConnectionString(configuration, "DefaultConnection");
        }

        public Task<Education> Add(Education student)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int studentId)
        {
            throw new NotImplementedException();
        }

        public async Task<Education> EducationForIdAsync(int Id)
        {
            Education education = new Education();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string SQL = "SELECT * FROM Educations WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(SQL, con)
                {
                    CommandType = System.Data.CommandType.Text,
                };
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                education = (await GetData(reader))[0];
                con.Close();
            }
            return education;
        }
        private async Task<List<Education>> GetData(SqlDataReader reader)
        {
            List<Education> lst = new List<Education>();
            //1. try catch verhindert applicatie crash
            try
            {
                while (await reader.ReadAsync())
                {
                    Education s = new Education();
                    s.Id = Convert.ToInt32(reader["Id"]);
                    s.Code = reader["Code"].ToString();
                    s.Name = reader["Name"].ToString();
                    s.Description = reader["Description"].ToString();
                    lst.Add(s);
                }
            }
            catch (Exception exc)
            {
                Console.Write(exc.Message); //later loggen
            }
            finally
            {
                reader.Close(); //Niet vergeten. Beperkt aantal verbindingen (of kosten)
            }
            return lst;
        }

        public Task<IEnumerable<Education>> EducationsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Education>> EducationsByEducationAsync(int educationId)
        {
            throw new NotImplementedException();
        }

        public Task<Education> Update(Education student)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Student>> GetAllEducationsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetStudentForIdAsync(int studentId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Student>> GetEducationsByEducationAsync(int educationId)
        {
            throw new NotImplementedException();
        }

        public Task<Student> Add(Student student)
        {
            throw new NotImplementedException();
        }

        public Task<Student> Update(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
*/