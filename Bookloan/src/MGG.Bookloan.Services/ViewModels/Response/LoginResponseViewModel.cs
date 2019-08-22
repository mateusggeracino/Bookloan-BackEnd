using System.Collections.Generic;
using System.Security.Claims;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace MGG.Bookloan.Services.ViewModels.Response
{
    public sealed class LoginResponseViewModel
    {
        public string SocialNumber { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }

        [JsonIgnore]
        public ValidationResult ValidationResult { get; set; }
        [JsonIgnore]
        public List<Claim> Claims { get; set; }
    }
}