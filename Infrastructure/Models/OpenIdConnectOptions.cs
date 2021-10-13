namespace Infrastructure.Models
{
    public class OpenIdConnectOptions
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Issuer { get; set; }

        public string Authority { get; set; }
    }
}