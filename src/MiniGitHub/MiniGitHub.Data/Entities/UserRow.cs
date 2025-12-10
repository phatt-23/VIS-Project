using System.Data.Common;
using MiniGitHub.Data.DataConnector.SqlConnector.Extensions;
using MiniGitHub.Data.Entities;

namespace MiniGitHub.Data.Rows;

public class UserRow : IIdentifialbeRow {
    public long Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public UserRow() {
    }
    
    public UserRow(long id, string username, string email, string password) { Id = id;
        Username = username;
        Email = email;
        Password = password;
    }

    public UserRow(DbDataReader reader) {
        Id = reader.Get<long>("user_id"); 
        Username = reader.Get<string>("username");
        Email = reader.Get<string>("email");
        Password = reader.Get<string>("password");
    }
    
    public override string ToString() {
        return $"UserId: {Id}, Username: {Username}, Email: {Email}, Password: {Password}";
    }
}