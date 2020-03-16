using Schooldb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schooldb.WebApp.Repositories
{
    public interface IEducationRepo
    {
        /*Task<IEnumerable<Education>> EducationsAsync();
        Task<Education> EducationForIdAsync(int studentId);
        Task<IEnumerable<Education>> EducationsByEducationAsync(int educationId);
        //CREATE (Async)
        Task<Education> Add(Education student);
        //UPDATE (Async)
        Task<Education> Update(Education student);
        //DELETE (Async)
        Task Delete(int studentId);*/

        Task<IEnumerable<Student>> GetAllEducationsAsync();
        Task<Student> GetStudentForIdAsync(int studentId);
        Task<IEnumerable<Student>> GetEducationsByEducationAsync(int educationId);
        Task<IEnumerable<Teacher>> GetAllEducationsByTeacher(int teacherId);
        //CREATE (Async)
        Task<Student> Add(Student student);
        //UPDATE (Async)
        Task<Student> Update(Student student);
        //DELETE (Async)
        Task Delete(int studentId);
    }
}
