using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using MyLibrary.Data.Services;
using MyLibrary.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyLibrary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyLibrary.Tests
{
    [TestFixture]
    public class BookTest
    {
        
        [Test]
        public async Task DeleteBookAsync_DeletesBookFromDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                           .UseInMemoryDatabase(databaseName: "DeleteBookDatabase")
                           .Options;

            using var context = new AppDbContext(options);
            var book = new Book { Name = "Test Book", Description = "Test Description" };
            context.Books.Add(book);
            await context.SaveChangesAsync();

            var service = new BooksService(context);

            // Act
            await service.DeleteAsync(book.Id);

            // Assert
            NUnit.Framework.Assert.IsFalse(context.Books.Any(b => b.Id == book.Id));
        }
    }
}
