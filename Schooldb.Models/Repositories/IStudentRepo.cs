using Schooldb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schooldb.WebApp.Repositories
{
    public interface IStudentRepo
    {
        //IStudentRepo.cs ---------------------
        //READ
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentForIdAsync(int studentId);
        Task<IEnumerable<Student>> GetStudentsByEducationAsync(int educationId);
        //CREATE (Async)
        Task<Student> Add(Student student);
        //UPDATE (Async)
        Task<Student> Update(Student student);
        //DELETE (Async)
        Task Delete(int studentId);
    }
}
