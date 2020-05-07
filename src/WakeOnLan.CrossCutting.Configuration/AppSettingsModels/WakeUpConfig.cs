namespace WakeOnLan.CrossCutting.Configuration.AppSettingsModels
{
    public sealed class WakeUpConfig
    {
        public MainWakeUp Main { get; set; }
        public ushort Port { get; set; }

        public sealed class MainWakeUp
        {
            public string Mac { get; set; }
            public string Ip { get; set; }
            public string Mask { get; set; }
        }
    }
}
