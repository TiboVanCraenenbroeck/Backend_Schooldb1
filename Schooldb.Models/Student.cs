using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Schooldb.Models
{
    public class Student: Person
    {
        public int? EducationId { get; set; }
        // Navigatieprops aanmaken
        [DisplayFormat(NullDisplayText ="Nog zonder studierichting")]
        public Education Education { get; set; }
    }
}
