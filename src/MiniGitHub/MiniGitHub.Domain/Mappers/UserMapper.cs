using MiniGitHub.Data.Rows;
using MiniGitHub.Domain.Entities;

namespace MiniGitHub.Domain.Mappers;

public class UserMapper : IMapper<UserRow, User> {
    public User MapFromRow(UserRow row) {
        return new User(row.Id, row.Username, row.Email, row.Password);
    }

    public UserRow MapToRow(User user) {
        return new UserRow(user.Id, user.Username, user.Email, user.Password);
    }
}