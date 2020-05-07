namespace WakeOnLan.CrossCutting.Configuration.AppSettingsModels
{
    public sealed class JwtConfig
    {
        public string Secret { get; set; }
        public double Expiration { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
    }
}
