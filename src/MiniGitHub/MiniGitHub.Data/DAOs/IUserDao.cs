using MiniGitHub.Data.Rows;

namespace MiniGitHub.Data.DAOs;

public interface IUserDao : IDao<UserRow> {
    UserRow GetByEmail(string email);
    UserRow GetByUsername(string username);
}