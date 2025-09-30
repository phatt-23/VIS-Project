namespace MiniGitHub.Domain.Entities;

public class User {
    public int Id {get;set;}
    public string Username {get;set;}
    public string Email {get;set;}
    public List<Repository> Repositories {get;set;} = new();

    public User(int id, string username, string email) {
        this.Id = id;
        this.Username = username;
        this.Email = email;
        this.Repositories = [];
    }

    public override string ToString() {
        return $"Id: {Id}, Username: {Username}, Email: {Email}, Repository Count: {Repositories.Count}";
    }
}