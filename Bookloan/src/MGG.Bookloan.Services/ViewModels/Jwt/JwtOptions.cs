namespace MGG.Bookloan.Services.ViewModels.Jwt
{
    public class JwtOptions
    {
        public string Secret { get; set; }
        public int ExpirationHours { get; set; }
        public string Issuer { get; set; }
        public string Valid { get; set; }
    }
}