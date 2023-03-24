using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyLibrary.Data;
using MyLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLibrary.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorsService _service;

        public AuthorsController(IAuthorsService service)
        {
            _service = service;
        }
        // Action method to display a list of authors on the index page.
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }
        // Action method to display the form for adding a new author.
        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }
        // Action method to create a new author.
        // POST: Authors/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")]Author author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }
            await _service.AddAsync(author);
            return RedirectToAction(nameof(Index));
        }

        // Action method to display details of a specific author.
        // GET: Authors/Details
        public async Task<IActionResult> Details(int id)
        {
            var authorDetails = await _service.GetByIdAsync(id);

            if (authorDetails == null) return View("NotFound");
            return View(authorDetails);
        }

        // Action method to display the form for editing an author.
        // GET: Authors/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var authorDetails = await _service.GetByIdAsync(id);
            if (authorDetails == null) return View("NotFound");
            return View(authorDetails);
        }
        // Action method to update an author's details.
        // POST: Authors/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Author author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }
            await _service.UpdateAsync(id, author);
            return RedirectToAction(nameof(Index));
        }

        // Action method to display the form for deleting an author.
        // GET: Authors/Delete
        public async Task<IActionResult> Delete(int id)
        {
            var authorDetails = await _service.GetByIdAsync(id);
            if (authorDetails == null) return View("NotFound");
            return View(authorDetails);
        }
        // Action method to delete an author.
        // POST: Authors/Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var authorDetails = await _service.GetByIdAsync(id);
            if (authorDetails == null) return View("NotFound");
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        // Method to retrieve the details of a specific author by their ID.
        public async Task<Author> GetAuthor(int id)
        {
            var authorDetails = await _service.GetByIdAsync(id);
            return authorDetails;
        }
    }
}
