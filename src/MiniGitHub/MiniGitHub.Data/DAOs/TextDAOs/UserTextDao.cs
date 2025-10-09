using MiniGitHub.Data.Rows;

namespace MiniGitHub.Data.DataAccessObjects.TextDAOs;

public class UserTextDao : IUserDao {
    public UserRow? GetById(int userId) => throw new NotImplementedException();

    public List<UserRow> GetAll() => throw new NotImplementedException();

    public UserRow? GetByEmail(string email) => throw new NotImplementedException();

    public UserRow GetByUsername(string username) => throw new NotImplementedException();

    public UserRow Insert(UserRow row) => throw new NotImplementedException();

    public UserRow Update(UserRow row) => throw new NotImplementedException();

    public bool Delete(int userId) => throw new NotImplementedException();
}