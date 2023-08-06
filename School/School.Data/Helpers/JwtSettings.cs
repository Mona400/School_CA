namespace School.Data.Helpers
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public string Issure { get; set; }
        public string Audience { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateLifetime { get; set; }
        public bool ValidateIssuerSignigKey { get; set; }
    }

}
