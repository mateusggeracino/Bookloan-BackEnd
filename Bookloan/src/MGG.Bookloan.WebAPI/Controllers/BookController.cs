using System;
using System.Linq;
using MGG.Bookloan.Infra;
using MGG.Bookloan.Services.Interfaces;
using MGG.Bookloan.Services.ViewModels.Request;
using MGG.Bookloan.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MGG.Bookloan.WebAPI.Controllers
{
    /// <summary>
    /// Controller responsável por prover comportamentos de book
    /// </summary>
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : BaseController
    {
        private readonly IBookServices _bookServices;
        private readonly ILogger<BookController> _logger;

        /// <summary>
        /// Método construtor de book
        /// </summary>
        /// <param name="bookServices">Contrato com os comportamentos de livros</param>
        /// <param name="logger">Contrato de logs</param>
        public BookController(IBookServices bookServices, ILogger<BookController> logger)
        {
            _bookServices = bookServices;
            _logger = logger;
        }

        /// <summary>
        /// Adicionar um novo livro
        /// </summary>
        /// <param name="book">Propriedades de livro</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<string> Post([FromBody]BookRequestViewModel book)
        {
            try
            {
                _logger.LogInformation("Add a new book");
                var result = _bookServices.Add(book);
                if (result.ValidationResult.Errors.Any()) return AddValidationErrors(result.ValidationResult.Errors);

                return Ok("success");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Obter todos os livros
        /// </summary>
        /// <returns>Retorna todos os livros cadastrados</returns>
        [HttpGet]
        public ActionResult<string> Get()
        {
            try
            {
                _logger.LogInformation("Get all books");
                var result = _bookServices.GetAll();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Atualização das propriedades de livro
        /// </summary>
        /// <param name="key">Chave do livro</param>
        /// <param name="book">Propriedades para serem atualizadas</param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<string> Put([FromHeader] Guid key, [FromBody] BookRequestViewModel book)
        {
            try
            {
                _logger.LogInformation("Atualização livro");
                var result = _bookServices.Update(key, book);
                if (result.ValidationResult.Errors.Any()) return AddValidationErrors(result.ValidationResult.Errors);

                return Ok(Labels.Success);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Exclui um livro cadastrado
        /// </summary>
        /// <param name="key">Chave do livro</param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult<string> Delete([FromHeader] Guid key)
        {
            try
            {
                _logger.LogInformation($"Delete a book: {key}");
                _bookServices.Inactivate(key);
                return Ok(Labels.Success);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                return new StatusCodeResult(500);
            }
        }
    }
}