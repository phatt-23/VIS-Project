using System.Data.Common;
using MiniGitHub.Data.Extensions;

namespace MiniGitHub.Data.Rows;

public class UserRow {
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public UserRow(DbDataReader reader) {
        UserId = reader.Get<int>("user_id"); 
        Username = reader.Get<string>("username");
        Email = reader.Get<string>("email");
        Password = reader.Get<string>("password");
    }

    public override string ToString() {
        return $"UserId: {UserId}, Username: {Username}, Email: {Email}, Password: {Password}";
    }
}