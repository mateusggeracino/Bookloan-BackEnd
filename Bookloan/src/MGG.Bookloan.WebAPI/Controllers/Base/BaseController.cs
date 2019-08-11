using System.Collections.Generic;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace MGG.Bookloan.WebAPI.Controllers.Base
{
    public class BaseController : Controller
    {
        protected ActionResult<string> AddValidationErrors(IList<ValidationFailure> validationErrors)
        {
            var listErrors = new List<string>();
            foreach (var error in validationErrors)
            {
                listErrors.Add(error.ErrorMessage);
            }
            return BadRequest(listErrors);
        }
    }
}