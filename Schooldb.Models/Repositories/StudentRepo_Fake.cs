using Schooldb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schooldb.WebApp.Repositories
{
    public class StudentRepo_Fake : IStudentRepo
    {
        private readonly IDataInitializer dataInitializer;

        public StudentRepo_Fake(IDataInitializer dataInitializer)
        {
            this.dataInitializer = dataInitializer;
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
            var result = dataInitializer.Students.OrderBy(s => s.Name);
            return await Task.FromResult(result);
        }

        public async Task<Student> GetStudentForIdAsync(int studentId)
        {
            //lambda methode Where() en extensiemethode FirstOrDefault() gebruiken
            var result = dataInitializer.Students.Where(s => s.Id == studentId).FirstOrDefault<Student>();
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<Student>> GetStudentsByEducationAsync(int educationId)
        {
            var result = dataInitializer.Students.Where(s => s.EducationId == educationId);
            return await Task.FromResult(result);
        }

        public Task<Student> Update(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
