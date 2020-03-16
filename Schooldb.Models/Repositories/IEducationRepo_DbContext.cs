using Schooldb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schooldb.WebApp.Repositories
{
    public interface IEducationRepo_DbContext
    {
        //READ: Task<IEnumerable<Teacher>> GetAllAsync(); Task<Teacher> GetForIdAsync(int id);
        // Task<IEnumerable<Teacher>> GetTeachersByEducationAsync(int educationId); bool TeacherExists(string name);
        //READ
        Task<IEnumerable<Education>> GetAllEducationsAsync();
        Task<IEnumerable<Education>> GetAllEducationsAsync(string search = null);
        Task<Education> GetEducationForIdAsync(int EducationId);
        Task<IEnumerable<Education>> GetAllEducationsByTeacher(int teacherId);

        //CREATE (Async)
        Task<Education> Add(Education Education);
        void AddEducationsToTeacher(int id, string[] selectedEducationsString);

        //UPDATE (Async)
        Task<Education> Update(Education Education);

        //DELETE (Async)
        Task Delete(int EducationId);
    }
}
