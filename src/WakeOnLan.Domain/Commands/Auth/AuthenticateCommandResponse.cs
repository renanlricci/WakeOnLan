namespace WakeOnLan.Domain.Commands.Auth
{
    public sealed class AuthenticateCommandResponse
    {
        public string BearerToken { get; private set; }

        public AuthenticateCommandResponse(string bearerToken)
        {
            BearerToken = bearerToken;
        }

        public static implicit operator AuthenticateCommandResponse(string bearerToken) =>
            new AuthenticateCommandResponse(bearerToken);
    }
}
