using System.Linq;
using MGG.Bookloan.Business.Interfaces;
using MGG.Bookloan.Domain.Entities;
using MGG.Bookloan.Domain.Validations;
using MGG.Bookloan.Repository.Interfaces;

namespace MGG.Bookloan.Business.Business
{
    public class BookBusiness : IBookBusiness
    {
        private readonly IBookRepository _bookRepository;

        public BookBusiness(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Book Add(Book book)
        {
            if (ClientValidator(book)) return book;

            return _bookRepository.Add(book);
        }

        private bool ClientValidator(Book book)
        {
            var bookValidator = new BookValidator();
            book.ValidationResult = bookValidator.Validate(book);
            if (book.ValidationResult.Errors.Any()) return true;

            return false;
        }
    }
}