using AutoMapper;
using Labb1_MinimalAPI.Data;
using Labb1_MinimalAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Labb1_MinimalAPI.Services {
    public class BookRepository : IRepository<Book> {

        private readonly DataContext _context;

        public BookRepository(DataContext context, IMapper mapper) {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAll() {
            return await _context.Books.ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetByTitle(string title) {
            return await _context.Books
                .Where(x => x.Title.ToLower()
                .Contains(title.ToLower()))
                .ToListAsync();
        }

        public async Task<Book> GetById(Guid id) {
            return await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Book> Create(Book entity) {
            await _context.Books.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Book> Update(Book entity) {
            var toUpdate = await _context.Books.FindAsync(entity.Id);
            if (toUpdate != null) {
                toUpdate.Title = entity.Title;
                toUpdate.Author = entity.Author;
                toUpdate.Genre = entity.Genre;
                toUpdate.Description = entity.Description;
                toUpdate.IsLoanAble = entity.IsLoanAble;

                await _context.SaveChangesAsync();
            }
            return toUpdate;
        }

        public async Task<Book> Delete(Guid id) {
            var toDelete = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);

            if (toDelete != null) {
                _context.Books.Remove(toDelete);
                await _context.SaveChangesAsync();
            }

            return toDelete;
        }

    }
}
