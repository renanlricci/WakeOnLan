namespace Raspberry.Configuration
{
    public sealed class Configs
    {
        public string AppName { get; set; }
        public JwtConfig JwtSettings { get; set; }
        public AuthConfig Auth { get; set; }
        public WakeUpConfig WakeUp { get; set; }
    }

    public sealed class AuthConfig
    {
        public string User { get; set; }
        public string Pass { get; set; }
    }

    public sealed class JwtConfig
    {
        public string Secret { get; set; }
        public double Expiration { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
    }

    public sealed class WakeUpConfig
    {
        public MainWakeUp Main { get; set; }
        public sealed class MainWakeUp
        {
            public string Mac { get; set; }
            public string Ip { get; set; }
            public string Mask { get; set; }
        }
    }
}
