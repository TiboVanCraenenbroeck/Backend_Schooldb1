using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Schooldb.Models;
using Schooldb.WebApp.Repositories;

namespace Schooldb.WebApp.Controllers
{
    public class EducationController : Controller
    {
        private readonly IEducationRepo_DbContext EducationRepo;

        public EducationController(IEducationRepo_DbContext educationRepo_DbContext)
        {
            this.EducationRepo = educationRepo_DbContext;
        }

        // GET: Education
        public async Task<ActionResult> Index()
        {
            return View(await EducationRepo.GetAllEducationsAsync());
        }

        // GET: Education/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();//standard taalafhankelijk browser Http Error 400
            }
            var education = await EducationRepo.GetEducationForIdAsync(id.Value);
            if (education == null)
            {
                return NotFound(); //Http Error 404
                //Of: Zorg voor een customised error (zie verder) 
            }
            return View(education);
        }

        public IActionResult Create()
        {
            return View();
        }

        // GET: Education/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection, Education education)
        {
            try
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
            }
        }

        /*// POST: Education/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }*/

        // POST: Education/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int? id, IFormCollection collection, Education education)
        {
            try
            {
                // TODO: Add insert logic here
                //Validatie komt later aan bod
                if (!ModelState.IsValid)
                {
                    throw new Exception("Validation Error");
                    //return BadRequest();                
                }
                if (id == null)
                {
                    //return BadRequest();
                    ModelState.AddModelError("", "Bad Request.");
                    return View(education); //return foutieve data
                }
                var result = await EducationRepo.Update(education);
                if (result == null)
                {
                    throw new Exception($"{education.Name} not found.");
                }
                return RedirectToAction(nameof(Index));

            }
            catch (Exception exc)
            {
                Debug.WriteLine($"Edit {education.Name} failed " + exc.Message);
                ModelState.AddModelError("", $"Update {education.Name} not succeeded.");
                return View(education); //teruggave foutief ingevulde view
            }

        }

        // GET: Education/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest(); //model nog null 
                }
                var education = await EducationRepo.GetEducationForIdAsync(id.Value);
                if (education == null)
                {
                    ModelState.AddModelError("", "Not found.");
                }
                return View(education);

                /*// TODO: Add update logic here

                return RedirectToAction(nameof(IndexAsync));*/
            }
            catch
            {
                return View();
            }
        }

        // GET: Education/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();
            var education = await EducationRepo.GetEducationForIdAsync(id.Value);
            if (education == null) { ModelState.AddModelError("", "Not Found."); }
            return View(education);
        }

        // POST: Education/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                if (id == null) { throw new Exception("Bad Delete Request"); }
                ////id check kan optioneel toegevoegd via het formulier
                await EducationRepo.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception exc)
            {
                //View :<div asp-validation-summary="All" class="text-danger">
                Debug.WriteLine($"Delete error. " + exc.Message);
                ModelState.AddModelError("", "Delete not succeeded." + exc.Message);
                return View();
            }
        }
/*        // POST: Education/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                if (id == null) { throw new Exception("Bad Delete Request"); }
                ////id check kan optioneel toegevoegd via het formulier
                await EducationRepo.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception exc)
            {
                //View :<div asp-validation-summary="All" class="text-danger">
                Debug.WriteLine($"Delete error. " + exc.Message);
                ModelState.AddModelError("", "Delete not succeeded." + exc.Message);
                return View();
            }
        }*/
    }
}