using System;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace MGG.Bookloan.Services.ViewModels.Response
{
    public class BookResponseViewModel
    {
        public Guid UniqueKey { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        [JsonIgnore]
        public ValidationResult ValidationResult { get; set; }
    }
}