using System;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace MGG.Bookloan.Services.ViewModels.Response
{
    public class ClientResponseViewModel
    {
        public Guid UniqueKey { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string SocialNumber { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        [JsonIgnore]
        public ValidationResult ValidationResult { get; set; }
    }
}