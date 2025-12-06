using MiniGitHub.Data.Rows;

namespace MiniGitHub.Data.DAOs;

public interface IUserDao {
    UserRow? GetById(long userId);
    List<UserRow> GetAll();
    UserRow? GetByEmail(string email);
    UserRow? GetByUsername(string username);
    UserRow Insert(UserRow row);
    UserRow? Update(UserRow row);
    bool Delete(long userId);
}