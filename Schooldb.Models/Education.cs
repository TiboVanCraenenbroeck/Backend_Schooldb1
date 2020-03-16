using Schooldb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schooldb.Models
{
    public class Education
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        //navigatie properties toevoegen
        //-- has many
        public ICollection<Student> Students { get; set; }
        //-- many-to-many
        public ICollection<TeachersEducations> TeachersEducations { get; set; }

    }
}
