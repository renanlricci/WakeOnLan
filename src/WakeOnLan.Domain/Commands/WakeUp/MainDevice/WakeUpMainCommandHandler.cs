using MediatR;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;
using WakeOnLan.CrossCutting.Configuration;
using WakeOnLan.Domain.Interfaces.Services;

namespace WakeOnLan.Domain.Commands.WakeUp.MainDevice
{
    public sealed class WakeUpMainCommandHandler : IRequestHandler<WakeUpMainCommand, WakeUpMainCommandResponse>
    {
        private readonly AppSettings _appSettings;
        private readonly IWakeOnLanService _wakeOnLanService;

        public WakeUpMainCommandHandler(IOptionsMonitor<AppSettings> appSettings, IWakeOnLanService wakeOnLanService)
        {
            _appSettings = appSettings.CurrentValue;
            _wakeOnLanService = wakeOnLanService;
        }

        public async Task<WakeUpMainCommandResponse> Handle(WakeUpMainCommand request, CancellationToken cancellationToken)
        {
            await _wakeOnLanService.WakeUp(_appSettings.WakeUp.Main.Mac, _appSettings.WakeUp.Main.Ip, _appSettings.WakeUp.Main.Mask, _appSettings.WakeUp.Port);
            return _appSettings.WakeUp.Main.Ip;
        }
    }
}
