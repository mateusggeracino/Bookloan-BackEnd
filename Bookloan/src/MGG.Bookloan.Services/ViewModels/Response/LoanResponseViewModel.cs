using System;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace MGG.Bookloan.Services.ViewModels.Response
{
    public sealed class LoanResponseViewModel
    {
        public Guid UniqueKey { get; set; }

        [JsonIgnore]
        public ValidationResult ValidationResult { get; set; }
    }
}