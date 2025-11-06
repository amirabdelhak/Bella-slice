using BL.manager;
using DAL.context;
using DAL.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Diagnostics;

namespace Presentation_layer.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly Icategorymanager categorymanager;
        public CategoriesController(Icategorymanager categorymanager)
        {
            this.categorymanager = categorymanager;
        }

        public IActionResult Index()
        {
            var categories = categorymanager.getallcategory() ?? new List<Category>();
            return View(categories);
        }

        public IActionResult Details(int id)
        {
            var model = categorymanager.getcategorybyid(id);
            if (model == null) return NotFound();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create( Category category)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    categorymanager.createcategory(category);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the category.");
                }
            }
            return View(category);
        }

        public IActionResult Edit(int id)
        {
            var model = categorymanager.getcategorybyid(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            // Trim whitespace-only input
            category.Name = category.Name?.Trim();
            if (string.IsNullOrWhiteSpace(category.Name))
            {
                ModelState.AddModelError(nameof(category.Name), "Name is required");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    categorymanager.updatecategory(category);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the category.");
                }
            }
            return View(category);
        }

        // Keep Delete as a GET shortcut but ensure it always tries to delete if id supplied
        public IActionResult Delete(int id)
        {
            if (id <= 0) return RedirectToAction("Index");
            try
            {
                categorymanager.deletecategory(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the category.");
            }
            return RedirectToAction("Index");
        }

    }
}
