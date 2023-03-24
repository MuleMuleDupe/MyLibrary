using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLibrary.Data;
using MyLibrary.Data.Services;
using MyLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLibrary.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooksService _service;
        private readonly IAuthorsService _authorService;

        public BooksController(IBooksService service, IAuthorsService authorService)
        {
            _service = service;
            _authorService = authorService;
        }
        public async Task<IActionResult> Index()
        {
            var allBooks = await _service.GetAllAsync();
            return View(allBooks);
        }

        //Get: Books/Create - Returns the view for creating a new book
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        // Post: Books/Create - Adds a new book to the database
        public async Task<IActionResult> Create([Bind("Name,Description,ReleaseDate,Price,Genre,ImageURL,AuthorId")] Book book)
        {
            // Check if the model state is not valid
            if (!ModelState.IsValid)
            {
                // Return the view with the invalid model
                return View(book);
            }
            await _service.AddAsync(book);
            return RedirectToAction(nameof(Index));
        }

        // Get: Books/Details - Returns the details of a book with the specified id
        public async Task<IActionResult> Details(int id)
        {
            var bookDetails = await _service.GetByIdAsync(id);

            if (bookDetails == null) return View("NotFound");
            return View(bookDetails);
        }

        // Get: Books/Edit - Returns the view for editing a book with the specified id
        public async Task<IActionResult> Edit(int id)
        {
            var bookDetails = await _service.GetByIdAsync(id);
            if (bookDetails == null) return View("NotFound");
            return View(bookDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,ReleaseDate,Price,Genre,ImageURL,AuthorId")] Book book)
        {
            // Check if the AuthorId in the book parameter is valid
            var author = await _authorService.GetByIdAsync(book.AuthorId);
            if (author == null)
            {
                ModelState.AddModelError("AuthorId", "Invalid author selected.");
            }

            // Check if the book is valid
            if (!ModelState.IsValid)
            {
                return View(book);
            }

            // Check if a book with the specified id exists in the database
            var existingBook = await _service.GetByIdAsync(id);
            if (existingBook == null)
            {
                return NotFound();
            }

            // Update the properties of the existing book with the values from the edited book
            existingBook.Name = book.Name;
            existingBook.Description = book.Description;
            existingBook.ReleaseDate = book.ReleaseDate;
            existingBook.Price = book.Price;
            existingBook.Genre = book.Genre;
            existingBook.ImageURL = book.ImageURL;
            existingBook.AuthorId = book.AuthorId;

            // Save the changes to the database
            await _service.UpdateAsync(id, existingBook);
            return RedirectToAction(nameof(Index));
        }

        //Get: Books/Delete
        // This action method retrieves the details of a book with a specific id and shows the details on the delete confirmation page
        // If the book is not found, it shows the "NotFound" view
        public async Task<IActionResult> Delete(int id)
        {
            var bookDetails = await _service.GetByIdAsync(id);
            if (bookDetails == null) return View("NotFound");
            return View(bookDetails);
        }

        [HttpPost, ActionName("Delete")]
        // This action method deletes the book with the specific id from the database
        // If the book is not found, it shows the "NotFound" view
        // After the book is deleted, it redirects to the index page
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookDetails = await _service.GetByIdAsync(id);
            if (bookDetails == null) return View("NotFound");
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
