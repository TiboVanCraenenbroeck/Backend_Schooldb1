using Microsoft.EntityFrameworkCore;
using Schooldb.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Schooldb.WebApp.Repositories
{
    public class EducationRepo_DbContex : IEducationRepo_DbContext
    {
        private readonly SchoolDBContext schoolDbContext;

        public EducationRepo_DbContex(SchoolDBContext schoolDBContext)
        {
            this.schoolDbContext = schoolDBContext;
        }

        public Task<IEnumerable<Teacher>> GetTeachersByEducationAsync(int educationId)
        {
            throw new NotImplementedException();
        }

        //Helpers  of extra functies --------------------------
        public async Task<bool> EducationExists(int id)
        {
            return await schoolDbContext.Educations.AnyAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Education>> GetAllEducationsAsync(string search = null)
        {
            IEnumerable<Education> result = null;  //null expliciet declareren;
            if (string.IsNullOrEmpty(search))
            {
                result = await schoolDbContext.Educations.ToListAsync();
            }
            else
            {
                var query = schoolDbContext.Educations.Where(e => e.Name.Contains(search) | e.Code.Contains(search));
                result = await query.ToListAsync();
            }
            return result.OrderBy(e => e.Name);
        }


        // Read
        public async Task<IEnumerable<Education>> GetAllEducationsAsync()
        {
            return await schoolDbContext.Educations.Include(e => e.Students).OrderBy(e => e.Name).ToListAsync();
            /* Eguals loading --> Onmiddelijk in klasse steken
             Voordeel -_> Gaat snel als je 1 tabel aan een andere linkt*/
        }
        public async Task<Education> GetEducationForAsync(int EducationId)
        {
            var result = await schoolDbContext.Educations.SingleOrDefaultAsync<Education>(e => e.Id == EducationId);
            return result;
        }

        public async Task<Education> Add(Education education)
        {
            /*try
            {
                var result = schoolDbContext.Educations.AddAsync(education); // ChangeTracker --> Iets dat in het geheugen bijhoudt wat je in de db gedaan hebt
                await schoolDbContext.SaveChangesAsync();
                *//*
                 * By Value
                 * By Reference --> Het argument dat je aan de methode geeft --> MOesten er wijziggingen op gebruern --> Db geeft het terug
                 *  --> Education kan veranderd zijn (bv ID = auto increment)
                 *//*
                //return result; --> Niet ok
                return education; // ByRef
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }*/
            try
            {
                var result = schoolDbContext.Educations.AddAsync(education); //ChangeTracking
                await schoolDbContext.SaveChangesAsync();//MUST !!!!
                                                         //return result; //NOK	
                return education;   //ByRef   -> autoIdentity ingevuld            
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.InnerException.Message);
                return null;
            }

        }

        public async Task<Education> GetEducationForIdAsync(int EducationId)
        {
            var result = await schoolDbContext.Educations.SingleOrDefaultAsync<Education>(e => e.Id == EducationId);
            return result;  //OrDefault omdat dit ook null returnt (foutcontrole)

        }

        public async Task<Education> Update(Education Education)
        {
            try
            {
                schoolDbContext.Educations.Update(Education);
                await schoolDbContext.SaveChangesAsync();
                return Education;
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
                return null;
            }
        }

        public async Task Delete(int EducationId)
        {
            try
            {
                Education education = await GetEducationForIdAsync(EducationId);

                if (education == null)
                {
                    return;
                }

                var result = schoolDbContext.Educations.Remove(education); //ChangeTracker
                //doe hier een archivering van education ipv delete -> veiliger
                await schoolDbContext.SaveChangesAsync();
                //of: return result.Entity.Id;

            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
            }
            return;

        }

        public async Task<IEnumerable<Education>> GetAllEducationsByTeacher(int teacherId)
        {
            List<Education> educations;
            try
            {

                var query = schoolDbContext.Educations.Include(e => e.TeachersEducations).ThenInclude(t => t.Teacher).Where(e => e.TeachersEducations.Any(t => t.TeacherId == teacherId));
                educations = await query.ToListAsync();
                return educations;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public void AddEducationsToTeacher(int id, string[] selectedEducationsString)
        {
            /*foreach (var edu in educations)
            {
                schoolDbContext.TeachersEducations.Add(new TeachersEducations { TeacherId = id, EducationId = Convert.ToInt32(edu) });
                schoolDbContext.SaveChanges();
            }*/
        }
    }
}
