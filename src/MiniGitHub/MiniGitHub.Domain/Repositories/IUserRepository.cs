using MiniGitHub.Domain.Entities;

namespace MiniGitHub.Domain.Repositories;

public interface IUserRepository {
    List<User> GetAllUsers();
    User? GetUserWithRepositories(int userId);
}