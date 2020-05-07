using WakeOnLan.CrossCutting.Configuration.AppSettingsModels;

namespace WakeOnLan.CrossCutting.Configuration
{
    public sealed class AppSettings
    {
        public string AppName { get; set; }
        public JwtConfig JwtSettings { get; set; }
        public AuthConfig Auth { get; set; }
        public WakeUpConfig WakeUp { get; set; }
        public PingConfig Ping { get; set; }
    }
}
