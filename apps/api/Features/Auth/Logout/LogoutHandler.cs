namespace Clinic.API.Features.Auth.Logout
{
    public static class LogoutHandler
    {
        public static Task<LogoutResponse> Handle(LogoutCommand command)
        {
            // For now, logout is handled by clearing the cookie in the AuthController.
            // If server-side token invalidation (e.g., blacklist) is needed, it would go here.
            return Task.FromResult(new LogoutResponse(true, "Logged out successfully."));
        }
    }
}
