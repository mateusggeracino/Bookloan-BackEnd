using System;
using System.Collections.Generic;
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

        public IEnumerable<Book> GetAll()
        {
            return _bookRepository.GetAll();
        }

        public Book GetByKey(Guid key)
        {
            return _bookRepository.GetByKey(key);
        }

        public Book Update(Book bookEntity)
        {
            return _bookRepository.Update(bookEntity);
        }

        public bool Inactivate(Book book)
        {
            book.Active = false;
            _bookRepository.Update(book);
            return true;
        }

        private bool ClientValidator(Book book)
        {
            var bookValidator = new BookValidator();
            book.ValidationResult = bookValidator.Validate(book);
            return book.ValidationResult.Errors.Any();
        }
    }
}