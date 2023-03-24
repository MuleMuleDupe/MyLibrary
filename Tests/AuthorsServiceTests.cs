using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyLibrary.Data;
using MyLibrary.Models;
using MyLibrary.Data.Services;
using Xunit;
using NUnit.Framework;

namespace MyLibrary.Tests
{
    public class AuthorsServiceTests
    {
        private readonly AppDbContext _context;
        private readonly AuthorsService _service;

        public AuthorsServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "MyLibraryTest")
                .Options;

            _context = new AppDbContext(options);
            _service = new AuthorsService(_context);
        }

        //[Fact]
        //public async Task GetAllAsync_ShouldReturnAllAuthors()
        //{
        //    // Arrange
        //    var authors = new List<Author>
        //    {
        //        new Author { FullName = "Author 1", Bio = "asdasdsa", ProfilePictureURL="2121.jpeg" },
        //        new Author { FullName = "Author 2", Bio = "asdasdsa", ProfilePictureURL="2121.jpeg" },
        //        new Author { FullName = "Author 3", Bio = "asdasdsa", ProfilePictureURL="2121.jpeg" }
        //    };

        //    await _context.AddRangeAsync(authors);
        //    await _context.SaveChangesAsync();

        //    // Act
        //    var result = await _service.GetAllAsync();
        //    var count = result.Count;

        //    // Assert
        //    Assert.AreEqual(3, count);
        //}

        [Fact]
        public async Task GetByIdAsync_ShouldReturnAuthorById()
        {
            // Arrange
            var author = new Author { FullName = "Author 1", Bio = "asdasdsa", ProfilePictureURL = "2121.jpeg" };
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetByIdAsync(author.Id);

            // Assert
            Assert.AreEqual(author.FullName, result.FullName);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNullIfAuthorNotFound()
        {
            // Arrange
            var authorId = 999;

            // Act
            var result = await _service.GetByIdAsync(authorId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateNewAuthor()
        {
            // Arrange
            var author = new Author { FullName = "Author 1", Bio = "asdasdsa", ProfilePictureURL = "2121.jpeg" };

            // Act
            await _service.AddAsync(author);

            // Assert
            var result = await _context.Authors.FirstOrDefaultAsync(a => a.Id == author.Id);
            Assert.NotNull(result);
            Assert.AreEqual(author.FullName, result.FullName);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateExistingAuthor()
        {
            // Arrange
            var author = new Author { FullName = "Author 1", Bio = "asdasdsa", ProfilePictureURL = "2121.jpeg" };
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();

            var updatedAuthor = new Author { Id = author.Id, FullName = "Updated Author", Bio = "ssss", ProfilePictureURL="sss.jpeg" };

            // Act
            await _service.UpdateAsync(updatedAuthor.Id, updatedAuthor);

            // Assert
            var result = await _context.Authors.FirstOrDefaultAsync(a => a.Id == author.Id);
            Assert.NotNull(result);
            Assert.AreEqual(updatedAuthor.FullName, result.FullName);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteAuthorById()
        {
            // Arrange
            var author = new Author { FullName = "Author 1", Bio = "asdasdsa", ProfilePictureURL = "2121.jpeg" };
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();

            // Act
            await _service.DeleteAsync(author.Id);

            // Assert
            var result = await _context.Authors.FirstOrDefaultAsync(a => a.Id == author.Id);
            Assert.Null(result);
        }
    }
}
