using System.Data;
using MiniGitHub.Data.Rows;
using MiniGitHub.Domain.Entities;

namespace MiniGitHub.Domain.DataMappers;

public class UserMapper {
    public UserMapper() {
    }

    public User MapUser(UserRow row) {
        return new User(row.UserId, row.Username, row.Email);
    }
}