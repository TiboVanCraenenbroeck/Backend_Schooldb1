using Schooldb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Schooldb.Models.Person;

namespace Schooldb.Models
{
    public class DataInitializer : IDataInitializer
    {
        //1. ctor
        public DataInitializer()
        {
            this.Educations = _educations.OrderBy(e => e.Code);
            this.Students = this.CreateFakeStudents(10);
        }
        //2. public properties opvraagbaar via IoC:
        public IEnumerable<Education> Educations { get; set; }
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
        //3. initialisaties: data invoeren in private Lists()
        //3.1. _educations
        private List<Education> _educations = new List<Education> {
         new Education {Id = 1,Code = "MCT", Name = "Media and Communication Technology"},
         new Education {Id =2,Code ="DEV", Name = "Degital Design and Development" },
         new Education {Id = 3, Code ="DAE", Name = "Digital Arts and Entertainment" }
         };
        //3.. _students
        private List<Student> _students = new List<Student>();
        private List<Student> CreateFakeStudents(int nmbrStudents)
        {
            for (var i = 1; i <= nmbrStudents; i++)
            {
                Student student = new Student();
                student.Id = i;
                //TODO: vul de properties verder aan
                student.Name = "NaamStudent" + i;
                student.Gender = (GenderType)new Random().Next(0, 2);

                student.EducationId = new Random().Next(1, _educations.Count());
                _students.Add(student);
            }
            return this._students;
        }
    }
}
