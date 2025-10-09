using MiniGitHub.Data.Rows;

namespace MiniGitHub.Data.DataAccessObjects;

public interface IUserDao {
    UserRow? GetById(int userId);
    List<UserRow> GetAll();
    UserRow? GetByEmail(string email);
    UserRow GetByUsername(string username);
    UserRow Insert(UserRow row);
    UserRow Update(UserRow row);
    bool Delete(int userId);
}