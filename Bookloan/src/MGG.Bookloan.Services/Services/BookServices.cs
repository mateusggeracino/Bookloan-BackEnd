using System;
using System.Collections.Generic;
using AutoMapper;
using MGG.Bookloan.Business.Interfaces;
using MGG.Bookloan.Domain.Entities;
using MGG.Bookloan.Services.Interfaces;
using MGG.Bookloan.Services.ViewModels.Request;
using MGG.Bookloan.Services.ViewModels.Response;

namespace MGG.Bookloan.Services.Services
{
    public class BookServices : IBookServices
    {
        private readonly IBookBusiness _bookBusiness;
        private readonly IMapper _mapper;

        public BookServices(IBookBusiness bookBusiness, IMapper mapper)
        {
            _bookBusiness = bookBusiness;
            _mapper = mapper;
        }

        public BookResponseViewModel Add(BookRequestViewModel book)
        {
            var bookEntity = _mapper.Map<Book>(book);
            var result = _bookBusiness.Add(bookEntity);

            return _mapper.Map<BookResponseViewModel>(result);
        }

        public IEnumerable<BookResponseViewModel> GetAll()
        {
            var books = _bookBusiness.GetAll();
            return _mapper.Map<IEnumerable<BookResponseViewModel>>(books);
        }

        public BookResponseViewModel Update(Guid key, BookRequestViewModel book)
        {
            var bookConsult = _bookBusiness.GetByKey(key);
            var bookEntity = _mapper.Map<Book>(book);
            bookEntity.UniqueKey = key;
            bookEntity.Id = bookConsult.Id;

            return _mapper.Map<BookResponseViewModel>(_bookBusiness.Update(bookEntity));
        }

        public bool Inactivate(Guid key)
        {
            var book = _bookBusiness.GetByKey(key);

            return _bookBusiness.Inactivate(book);
        }
    }
}