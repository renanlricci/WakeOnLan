using Raspberry.Configuration;

namespace Raspberry.Services
{
    public sealed class SimpleAuthService
    {
        private readonly AuthConfig _configs;

        public SimpleAuthService(AuthConfig configs)
        {
            _configs = configs;
        }

        public bool ValidateUser(string username, string password)
        {
            if (_configs.User.Equals(username) && _configs.Pass.Equals(password))
                return true;
            else return false;
        }
    }
}
