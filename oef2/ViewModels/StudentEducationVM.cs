using Microsoft.AspNetCore.Mvc.Rendering;
using Schooldb.Models;
using Schooldb.WebApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schooldb.WebApp.ViewModels
{
    public class StudentEducationVM
    {
        public Student Student { get; }
        public SelectList ListEducation { get; }

        public StudentEducationVM(IEducationRepo_DbContext educationRepo, Student student)
        {
            this.Student = student;
            //Selectlist houdt geselecteerde waarde bij.(EducationId) 
            this.ListEducation = new SelectList(educationRepo.GetAllEducationsAsync().Result, "Id", "Name", student.EducationId);
        }

    }
}
