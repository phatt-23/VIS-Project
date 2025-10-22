namespace MiniGitHub.Domain.Entities;

public class User {
    public long UserId {get;set;}
    public string Username {get;set;}
    public string Email {get;set;}
    public string Password {get;set;}
    public List<Repository> Repositories {get;set;} = new();
    
    public User(long userId, string username, string email, string password) {
        this.UserId = userId;
        this.Username = username;
        this.Email = email;
        this.Password = password;
        this.Repositories = [];
    }

    public override string ToString() {
        return $"Id: {UserId}, Username: {Username}, Email: {Email}, Repository Count: {Repositories.Count}";
    }
}