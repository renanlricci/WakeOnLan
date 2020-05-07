using MediatR;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;
using WakeOnLan.CrossCutting.Configuration;
using WakeOnLan.CrossCutting.Exceptions;
using WakeOnLan.Domain.Interfaces.Services;

namespace WakeOnLan.Domain.Commands.Auth
{
    public sealed class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, AuthenticateCommandResponse>
    {
        private readonly AppSettings _appSettings;
        private readonly ITokenGeneratorService _tokenGeneratorService;

        public AuthenticateCommandHandler(IOptionsMonitor<AppSettings> appSettings, ITokenGeneratorService tokenGeneratorService)
        {
            _appSettings = appSettings.CurrentValue;
            _tokenGeneratorService = tokenGeneratorService;
        }
         
        public async Task<AuthenticateCommandResponse> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            if (!request.UserName.Equals(_appSettings.Auth.User) || !request.PassWord.Equals(_appSettings.Auth.Pass))
                throw new UserNotAuthenticatedException();

            return await _tokenGeneratorService.GenerateToken(request.UserName, null); ;
        }

        
    }
}
