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


        public BooksController(IBooksService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allBooks = await _service.GetAllAsync();
            return View(allBooks);
        }

        //Get: Books/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Description,ReleaseDate,Price,Genre,ImageURL,AuthorId")] Book book)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }
            await _service.AddAsync(book);
            return RedirectToAction(nameof(Index));
        }

        //Get: Authors/Details
        public async Task<IActionResult> Details(int id)
        {
            var bookDetails = await _service.GetByIdAsync(id);

            if (bookDetails == null) return View("NotFound");
            return View(bookDetails);
        }

        //Get: Authors/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var bookDetails = await _service.GetByIdAsync(id);
            if (bookDetails == null) return View("NotFound");
            return View(bookDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,ReleaseDate,Price,Genre,ImageURL,AuthorId")] Book book)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }
            await _service.UpdateAsync(id, book);
            return RedirectToAction(nameof(Index));
        }

        //Get: Authors/Delete
        public async Task<IActionResult> Delete(int id)
        {
            var bookDetails = await _service.GetByIdAsync(id);
            if (bookDetails == null) return View("NotFound");
            return View(bookDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookDetails = await _service.GetByIdAsync(id);
            if (bookDetails == null) return View("NotFound");
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
