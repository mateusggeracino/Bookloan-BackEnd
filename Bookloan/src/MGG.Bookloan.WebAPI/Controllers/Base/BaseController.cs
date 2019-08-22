using System;
using System.Collections.Generic;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace MGG.Bookloan.WebAPI.Controllers.Base
{
    /// <summary>
    /// controller base para todas as outras herdarem
    /// </summary>
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Propriedades que é preenchida com o token do sistema
        /// </summary>
        protected Guid ClientKey => User  == null ? Guid.Empty : Guid.Parse(User.FindFirst("ClientKey").Value);

        /// <summary>
        /// Adiciona erros do validationfailure (fluentvalidator) em um array de string e retorna como badrequest
        /// </summary>
        /// <param name="validationErrors">IList e erros do fluentvalidator</param>
        /// <returns></returns>
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