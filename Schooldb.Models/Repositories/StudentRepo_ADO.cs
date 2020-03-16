using Microsoft.Extensions.Configuration;
using Schooldb.Models;
using Schooldb.WebApp.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Schooldb.Repositories
{
    public class StudentRepo_ADO: IStudentRepo
    {
        private readonly string connectionString;
        private readonly IEducationRepo educationRepo;

        public StudentRepo_ADO(IConfiguration configuration, IEducationRepo educationRepo)
        {
            connectionString = ConfigurationExtensions.GetConnectionString(configuration, "DefaultConnection");
            this.educationRepo = educationRepo;
        }

        public Task<Student> Add(Student student)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int studentId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            List<Student> lst = new List<Student>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //1. SQL query
                string sql = "SELECT * FROM Students ";
                SqlCommand cmd = new SqlCommand(sql, con);
                //2. Data ophalen
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                lst = await GetData(reader);
                con.Close();
            }
            return lst;
        }
        public async Task<Student> GetStudentForIdAsync(int id)
        {
            Student student = new Student();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string SQL = "SELECT * FROM Students WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(SQL, con)
                {
                    CommandType = System.Data.CommandType.Text,
                };
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                student = (await GetData(reader))[0];
                con.Close();
            }
            return student;
        }

        public Task<IEnumerable<Student>> GetStudentsByEducationAsync(int educationId)
        {
            throw new NotImplementedException();
        }

        public Task<Student> Update(Student student)
        {
            throw new NotImplementedException();
        }

        //SQL helpers --------------------------------------------------
        private async Task<List<Student>> GetData(SqlDataReader reader)
        {
            List<Student> lst = new List<Student>();
            //1. try catch verhindert applicatie crash
            try
            {
                while (await reader.ReadAsync())
                {
                    Student s = new Student();
                    s.Id = Convert.ToInt32(reader["Id"]);
                    s.Name = !Convert.IsDBNull(reader["Name"]) ? (string)reader["Name"]: "";
                    //TO DO: verder uitbouwen van overige properties
                    /*s.Email = !Convert.IsDBNull(reader["Email"]) ? (string)reader["Email"] : "";
                    s.Password = !Convert.IsDBNull(reader["Password"]) ? (string)reader["Password"] : "";*/
                    s.Gender = Convert.ToInt32(reader["Gender"]) == 0 ? Person.GenderType.Male : Person.GenderType.Female;
                    if (!Convert.IsDBNull(reader["DateOfBirth"]))
                    {
                        s.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                    }
                    if (!Convert.IsDBNull(reader["EducationId"]))
                    {
                        s.EducationId = (int)reader["EducationId"];
                        //s.Education = await educationRepo EducationForIdAsync(s.EducationId.Value);
                    }
                    //Let op mogelijke NULL waarden (=> anders crash)
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
    }
}
