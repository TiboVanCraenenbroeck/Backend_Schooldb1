using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Schooldb.Models;
using Schooldb.WebApp.Repositories;
using Schooldb.WebApp.ViewModels;

namespace Schooldb.WebApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepo studentRepo;
        private readonly IEducationRepo_DbContext educationRepo;

        public StudentController(IStudentRepo studentRepo, IEducationRepo_DbContext educationRepo)
        {
            this.studentRepo = studentRepo;
            this.educationRepo = educationRepo;
        }

        public async Task<IActionResult> Index(string educationSearch = null)
        {
            ViewBag.ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.search = educationSearch;

            List<Student> result = new List<Student>();

            if (string.IsNullOrEmpty(educationSearch))
            {
                result = (await studentRepo.GetAllStudentsAsync()).ToList();
            }
            else
            {
                var educations = await educationRepo.GetAllEducationsAsync(educationSearch);
                // Alle mogelijkheden Testen --> Zeker null dus!!!
                if (educations == null)
                {
                    return View("_NotFound");
                }

                foreach (Education education in educations)
                {
                    var resultStudents = await studentRepo.GetStudentsByEducationAsync(education.Id);
                    foreach (var student in resultStudents)
                    {
                        result.Add(student);
                    }
                }
            }
            return View("Index", result);
        }
        public IActionResult Form_StudentsForEducation()
        {
            return View();
        }
        public async Task<IActionResult> DetailsAsync(int? id)
        {
            if (id == null)
            {
                return BadRequest(); //400
            }
            var result = await studentRepo.GetStudentForIdAsync(id.Value);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            List<Education> educations = new List<Education>();
            educations.Insert(0, new Education { Name = "--- Kies een opleiding ---" });
            educations.AddRange(await educationRepo.GetAllEducationsAsync());
            //var educationList = await educationRepo.GetAllEducationsAsync();
            ViewData["Educations"] = new SelectList(educations, "Id", "Name");
            return View(new Student());
        }

        // GET: Education/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection, [Bind("DateOfBirth,Email,Name,Gender,Password,EducationId")] Student student)
        {
            try
            {
                List<Education> educations = new List<Education>();
                educations.Insert(0, new Education { Name = "--- Kies een opleiding ---" });
                educations.AddRange(await educationRepo.GetAllEducationsAsync());

                ViewData["Educations"] = new SelectList(educations, "Id", "Name", student.EducationId);
                if (ModelState.IsValid)
                {
                    student.EducationId = (student.EducationId == 0) ? null : student.EducationId;
                }
                else
                {
                    return View(student);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Create is unable to save. " + ex.Message);
                throw;
            }
            /*try
            {
                if (!ModelState.IsValid)
                {
                    //return BadRequest();
                    throw new Exception("Validation Error");
                }
                var created = await EducationRepo.Add(education);
                if (created == null)
                {
                    throw new Exception("Invalid Entry");
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Create is giving an error: " + ex.Message);
                ModelState.AddModelError("", "Create actie is mislukt voor " + education.Name); // Wat je aan de gebruiker teruggeeft
                return View();
            }*/
            return View();
        }
        public async Task<IActionResult> EditAsync(int? id)
        {
            if (id == null)
            {
                return BadRequest(); //400
            }
            var student = await studentRepo.GetStudentForIdAsync(id.Value);
            if (student == null)
            {
                return NotFound();
            }
            StudentEducationVM vm = new StudentEducationVM(educationRepo, student);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, StudentEducationVM vm)
        {
            vm = new StudentEducationVM(educationRepo, vm.Student);
            return View(vm);
        }
    }
}