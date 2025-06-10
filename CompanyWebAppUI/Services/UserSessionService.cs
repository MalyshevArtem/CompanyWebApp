using CompanyWebAppUI.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace CompanyWebAppUI.Services
{
    public class UserSessionService
    {
        private readonly ProtectedLocalStorage _storage;
        private const string _sessionKey = "user";

        public UserModel? User { get; private set; }
        public bool IsAuthorized => User is not null;

        public UserSessionService(ProtectedLocalStorage storage)
        {
            _storage = storage;
        }

        public async Task LoadSessionAsync()
        {
            var result = await _storage.GetAsync<UserModel>(_sessionKey);

            if (result.Success)
            {
                User = result.Value;
            }
        }

        public async Task AuthorizeAsync(UserModel user)
        {
            User = user;
            await _storage.SetAsync(_sessionKey, user);
        }

        public async Task LogoutAsync()
        {
            User = null;
            await _storage.DeleteAsync(_sessionKey);
        }
    }
}
