using System.Text.Json;
using MiniGitHub.Data.Rows;

namespace MiniGitHub.Data.DataAccessObjects.TextDAOs;

public class UserTextDao : IUserDao {
    public UserTextDao(string path) {
        _path = path;
    }

    public UserRow? GetById(int userId) {
        var users = GetAll().Where(u => u.UserId == userId).ToList();
        if (!users.Any()) {
            return null;
        }
        return users.First();
    }

    public List<UserRow> GetAll() {
        List<UserRow>? users = JsonSerializer.Deserialize<List<UserRow>>(File.ReadAllText(_path));
        if (users == null) {
            throw new InvalidOperationException($"File with users wasn't read properly: {_path}");
        }

        return users;
    }

    public UserRow? GetByEmail(string email) {
        var users = GetAll().Where(u => u.Email == email).ToList();
        if (!users.Any()) {
            return null;
        }
        return users.First();        
    }

    public UserRow GetByUsername(string username) {
        var users = GetAll().Where(u => u.Username == username).ToList();
        if (!users.Any()) {
            return null;
        }
        return users.First();        
    }

    public UserRow Insert(UserRow row) {
        var users = GetAll();

        // todo: add a random gen key, instead of just length of the previous list
        row.UserId = users.Count;
        users.Add(row);
        
        string serialized = JsonSerializer.Serialize(users);
        File.WriteAllText(_path, serialized);
        return row;
    }

    public UserRow Update(UserRow row) => throw new NotImplementedException();

    public bool Delete(int userId) => throw new NotImplementedException();

    private readonly string _path;
}
