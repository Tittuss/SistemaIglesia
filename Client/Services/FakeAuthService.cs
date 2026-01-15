namespace Client.Services
{
    public class FakeAuthService
    {
        public event Action? OnChange;

        public bool IsLoggedIn { get; private set; } = false;
        public Guid CurrentUserId { get; private set; }
        public string CurrentUserName { get; private set; } = string.Empty;
        public string CurrentRole { get; private set; } = string.Empty;

        public void Login(Guid userId, string name, string role)
        {
            IsLoggedIn = true;
            CurrentUserId = userId;
            CurrentUserName = name;
            CurrentRole = role;
            NotifyStateChanged();
        }

        public void Logout()
        {
            IsLoggedIn = false;
            CurrentUserId = Guid.Empty;
            CurrentUserName = string.Empty;
            CurrentRole = string.Empty;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
