using Microsoft.AspNetCore.Mvc.Rendering;
using Schooldb.Models;
using Schooldb.WebApp.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Schooldb.WebApp.ViewModels
{
    public class TeacherEducationsVM
    {
        public TeacherEducationsVM(IEducationRepo_DbContext educationRepo, Teacher teacher, IEnumerable<Education> SelectedEducations)
        {
            Teacher = teacher;
            this.SelectedEducations = SelectedEducations;
            this.ListEducations = new MultiSelectList(educationRepo.GetAllEducationsAsync().Result, "Education.Id", "Education.Name", SelectedEducations);
        }
        public Teacher Teacher { get; set; }
        public IEnumerable<Education> SelectedEducations { get; }

        [Display(Name = "Eén of meerdere opleidingen")]
        public MultiSelectList ListEducations { get; set; }
        //public List<Education> ListEducations { get; set; }

        [Required(ErrorMessage = "Kies minstens één opleiding ")]
        public string[] SelectedEducationsString { get; set; } //helper voor HTTP
    }
}
