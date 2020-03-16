using Microsoft.EntityFrameworkCore;
using Schooldb.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Schooldb.WebApp.Repositories
{
    public class TeacherRepo : ITeacherRepo
    {
        private readonly SchoolDBContext _schoolDBContext;

        public TeacherRepo(SchoolDBContext schoolDBContext)
        {
            _schoolDBContext = schoolDBContext;
        }
        public async Task<IEnumerable<Teacher>> GetAllAsync()
            => await _schoolDBContext.Teachers.Include(s => s.TeachersEducations).ThenInclude(e => e.Education).OrderBy(t => t.Name).ToListAsync();
        public async Task<Teacher> GetForIdAsync(int id)
        {
            return await _schoolDBContext.Teachers.FirstOrDefaultAsync(t => t.Id == id);
        }

        public Task<IEnumerable<Teacher>> GetTeachersByEducationAsync(int educationId)
        {
            throw new NotImplementedException();
        }

        public bool TeacherExists(string name)
        {
            throw new NotImplementedException();
        }
        public async Task<Teacher> Add(Teacher teacher)
        {
            try
            {
                var result = _schoolDBContext.Teachers.AddAsync(teacher);//ChangeTracking
                await _schoolDBContext.SaveChangesAsync();
                return teacher; //heeft nu een id (autoidentity)
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.InnerException.Message);
                return null;
            }
        }
        public async Task<Teacher> Update(Teacher Teacher)
        {
            try
            {
                _schoolDBContext.Teachers.Update(Teacher);
                await _schoolDBContext.SaveChangesAsync();
                return Teacher;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                Teacher teacher = await _schoolDBContext.Teachers.FindAsync(id);
                if (teacher == null) { return; }
                var result = _schoolDBContext.Teachers.Remove(teacher); //ChangeTracker
                //doe hier een archivering van teacher ipv delete -> veiliger
                await _schoolDBContext.SaveChangesAsync();
                //of: return result.Entity.Id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return;
        }
    }
}
