using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Schooldb.Models
{
    public class Teacher : Person
    {
        [DataType(DataType.EmailAddress, ErrorMessage = "Dit is geen geldig datatype")]
        [RegularExpression(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$", ErrorMessage ="Dit is geen geldig mailadres")]
        public string Email { get; set; }
        public ICollection<TeachersEducations> TeachersEducations { get; set; }
    }
}
