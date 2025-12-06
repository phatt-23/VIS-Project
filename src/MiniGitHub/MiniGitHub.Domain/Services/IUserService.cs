using MiniGitHub.Domain.Entities;

namespace MiniGitHub.Domain.Services;

public interface IUserService {
    List<User> GetAllUsers();
    User? GetUserWithRepositories(long userId);
    User AddUser(User user);  // throws
    bool UserWithEmailExists(string email);
    bool UserWithUsernameExists(string username);
    User? GetUserByUsername(string dtoUsernameOrEmail);
    User? GetUserByEmail(string dtoUsernameOrEmail);
    User? GetUserById(long userId);
    User? UpdateUser(User user);
}