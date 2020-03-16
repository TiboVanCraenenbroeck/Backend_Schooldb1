using Schooldb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schooldb.WebApp.Repositories
{
    public class StudentRepo: IStudentRepo
    {
        public StudentRepo()
        {
        }

        public Task<Student> Add(Student student)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int studentId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetStudentForIdAsync(int studentId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Student>> GetStudentsByEducationAsync(int educationId)
        {
            throw new NotImplementedException();
        }

        public Task<Student> Update(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
