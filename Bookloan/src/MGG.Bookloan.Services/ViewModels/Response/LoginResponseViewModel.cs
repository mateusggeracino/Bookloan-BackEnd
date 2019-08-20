using FluentValidation.Results;
using Newtonsoft.Json;

namespace MGG.Bookloan.Services.ViewModels.Response
{
    public class LoginResponseViewModel
    {
        public string SociaNumber { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }

        [JsonIgnore]
        public ValidationResult ValidationResult { get; set; }
    }
}