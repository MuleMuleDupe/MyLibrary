using MyLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLibrary.Data
{
    public interface IAuthorsService
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author> GetByIdAsync(int id);
        Task AddAsync(Author author);
        Task<Author> UpdateAsync(int id, Author newAuthor);
        Task DeleteAsync(int id);
    }
}
