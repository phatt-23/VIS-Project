using System.Data.Common;
using MiniGitHub.Data.Rows;

namespace MiniGitHub.Data.DAOs.SqlDAOs;

public class UserSqlDao(DbConnection connection) : IUserDao {
    public List<UserRow> GetAll()
    {
        List<UserRow> rows = new();
        var call = new SqlDatabaseCall(connection);

        var r = call.ExecuteReader(@"SELECT * FROM z_user", new());
        while (r.Read())
        {
            rows.Add(new UserRow(r));
        }

        return rows;
    }

    public UserRow GetById(long userId)
    {
        var call = new SqlDatabaseCall(connection);

        DbDataReader r = call.ExecuteReader(
            @"SELECT * FROM z_user WHERE user_id = @user_id",
            new() {
                {"@user_id", userId},
            });

        Dictionary<string, object> row = new Dictionary<string, object>();
        if (r.Read())
        {
            return new UserRow(r);
        }

        throw new Exception("User not found");
    }

    public UserRow GetByEmail(string email)
    {
        var call = new SqlDatabaseCall(connection);

        DbDataReader r = call.ExecuteReader(
            @"SELECT * FROM z_user WHERE email = @email",
            new() {
                {"@email", email},
            });

        if (r.Read())
        {
            return new UserRow(r);
        }

        throw new Exception("User not found");
    }

    public UserRow GetByUsername(string username)
    {
        var db = new SqlDatabaseCall(connection);

        var r = db.ExecuteReader(
            @"SELECT * FROM z_user WHERE username = @username",
            new() {
                {"@username", username},
            });

        if (r.Read())
        {
            return new UserRow(r);
        }

        throw new Exception("User not found");
    }

    public UserRow Insert(UserRow row)
    {
        var call = new SqlDatabaseCall(connection);

        var r = call.ExecuteScalar(
            @"INSERT INTO z_user
                     (username, email, password)
                  VALUES 
                     (@username, @email, @password)
                  RETURNING user_id",
            new() {
                {"@username", row.Username},
                {"@email", row.Email},
                {"@password", row.Password},
            }
        );

        if (r is not null)
        {
            row.Id = (long)r;
            return row;
        }

        throw new InvalidOperationException("Unable to insert new user");
    }

    public UserRow Update(UserRow row)
    {
        var call = new SqlDatabaseCall(connection);

        var r = call.ExecuteScalar(
            @"UPDATE z_user
              SET
                username = @username,
                email = @email,
                password = @password
              WHERE user_id = @id
              RETURNING user_id",
            new() {
                {"@username", row.Username},
                {"@email", row.Email},
                {"@password", row.Password},
            });

        if (r is not null) {
            return row;
        }

        throw new InvalidOperationException("Unable to update user.");
    }

    public bool Delete(long userId)
    {
        var db = new SqlDatabaseCall(connection);

        int nrows = db.ExecuteNonQuery(@"DELETE FROM z_user WHERE user_id = @user_id", new() {
                {"@user_id", userId},
            });
            
        if (nrows > 0)
        {
            return true;
        }

        return false;
    }
}
