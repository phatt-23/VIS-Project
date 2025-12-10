namespace MiniGitHub.Domain.Services;

public interface IAuthService {
    bool ValidateLogin(string usernameOrEmail, string password);
}