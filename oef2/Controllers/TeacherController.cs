using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Schooldb.Models;
using Schooldb.WebApp.Repositories;
using Schooldb.WebApp.ViewModels;

namespace Schooldb.WebApp.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ITeacherRepo _teacherRepo;
        private readonly IEducationRepo_DbContext educationRepo;

        public TeacherController(ITeacherRepo teacherRepo, IEducationRepo_DbContext educationRepo)
        {
            _teacherRepo = teacherRepo;
            this.educationRepo = educationRepo;
        }

        // GET: Teacher
        public async Task<ActionResult> Index()
        {
            return View(await _teacherRepo.GetAllAsync());
        }

        // GET: Teacher/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();//standard taalafhankelijk browser Http Error 400
            }
            var education = await _teacherRepo.GetForIdAsync(id.Value);
            if (education == null)
            {
                //return NotFound(); //Http Error 404
                //Of: Zorg voor een customised error (zie verder) 
                Response.Redirect("/Error/404");
                return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
            }
            return View(education);
        }

        // GET: Teacher/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Teacher/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Teacher teacher)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    throw new Exception("Validation Error");
                }
                Teacher result = await _teacherRepo.Add(teacher);
                if (result == null)
                    throw new Exception("Invalid Entry");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Create is unable to save." + ex.Message);
                ModelState.AddModelError("CreateError", "Create mislukt. " + ex.Message);
                return View(teacher); //foutieve view teruggeven
            }
        }

        // GET: Teacher/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();//standard taalafhankelijk browser Http Error 400
            }
            var teacher = await _teacherRepo.GetForIdAsync(id.Value);
            if (teacher == null)
            {
                return NotFound(); //Http Error 404
                //Of: Zorg voor een customised error (zie verder) 
            }
            IEnumerable<Education> SelectedEducations = await educationRepo.GetAllEducationsByTeacher(teacher.Id);
            TeacherEducationsVM vm = new TeacherEducationsVM(educationRepo, teacher, SelectedEducations);
            return View(vm);
        }

        // POST: Teacher/Edit/5
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int? id, IFormCollection collection, TeacherEducationsVM vm)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    throw new Exception("Validation Error");
                    //return BadRequest();                
                }
                if (id == null)
                {
                    //return BadRequest();
                    ModelState.AddModelError("", "Bad Request.");
                    return View(teacher); //return foutieve data
                }
                var result = await _teacherRepo.Update(teacher);
                if (result == null)
                {
                    throw new Exception("Not Found.");
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Edit {teacher.Name} failed " + ex.Message);
                ModelState.AddModelError("", $"Update {teacher.Name} not succeeded.");
                return View(teacher); //teruggave foutief ingevulde view
            }
        }*/

        // GET: Teacher/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return BadRequest();//standard taalafhankelijk browser Http Error 400
            }
            var teacher = await _teacherRepo.GetForIdAsync(id.Value);
            if (teacher == null)
            {
                return NotFound(); //Http Error 404
                //Of: Zorg voor een customised error (zie verder) 
            }
            return View(teacher);
        }

        // POST: Teacher/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                if (id == null) { throw new Exception("Bad Delete Request"); }
                await _teacherRepo.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                //View :<div asp-validation-summary="All" class="text-danger">
                Debug.WriteLine("Delete error. " + ex.Message);
                ModelState.AddModelError(String.Empty, "Delete failed.");
                return View();
            }
        }
    }
}