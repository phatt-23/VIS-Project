namespace MiniGitHub.Domain.Entities;

public class User : IIdentifiableObject {
    public long Id {get;set;}
    public string Username {get;set;}
    public string Email {get;set;}
    public string Password {get;set;}
    public List<Repository> Repositories {get;set;}
    
    public User(long id, string username, string email, string password) {
        this.Id = id;
        this.Username = username;
        this.Email = email;
        this.Password = password;
        this.Repositories = [];
    }

    public override string ToString() {
        return $"Id: {Id}, Username: {Username}, Email: {Email}, Repository Count: {Repositories.Count}";
    }
}