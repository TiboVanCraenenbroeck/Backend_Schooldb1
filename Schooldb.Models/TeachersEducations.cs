using Schooldb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schooldb.Models
{
    public class TeachersEducations
    {
        public int TeacherId { get; set; }
        public int EducationId { get; set; }
        // Navigatie propr - many to many
        public Teacher Teacher { get; set; }
        public Education Education { get; set; }
    }
}
