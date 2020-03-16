using Schooldb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schooldb.WebApp.Repositories
{
    public interface ITeacherRepo
    {
        Task<IEnumerable<Teacher>> GetAllAsync();
        Task<Teacher> GetForIdAsync(int id);
        Task<IEnumerable<Teacher>> GetTeachersByEducationAsync(int educationId);
        bool TeacherExists(string name);
        Task<Teacher> Add(Teacher teacher);
        Task<Teacher> Update(Teacher teacher);
        Task Delete(int id);
    }
}
