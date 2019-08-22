namespace MGG.Bookloan.Services.ViewModels.Jwt
{
    public sealed class JwtOptions
    {
        public string Secret { get; set; }
        public int ExpirationInHours { get; set; }
        public string Issuer { get; set; }
        public string ValidIn { get; set; }
    }
}