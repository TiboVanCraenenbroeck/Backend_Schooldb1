using Schooldb.Models;
using System.Collections.Generic;

namespace Schooldb.Models
{
    public interface IDataInitializer
    {
        IEnumerable<Education> Educations { get; set; }
        IEnumerable<Student> Students { get; set; }
        IEnumerable<Teacher> Teachers { get; set; }
    }
}