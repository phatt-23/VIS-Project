using System.Data;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using MiniGitHub.Data.DAOs;
using MiniGitHub.Data.DAOs.SqlDAOs;
using MiniGitHub.Data.Rows;
using static MiniGitHub.Data.DataConnector.SqlConnector.Extensions.DbCommandExtensions;

namespace MiniGitHub.Data.DataAccessObjects;

public class UserSqlDao : IUserDao
{
    public UserSqlDao(DbConnection connection)
    {
        _connection = connection;
    }

    public List<UserRow> GetAll()
    {
        List<UserRow> rows = new();
        var call = new SqlDatabaseCall(_connection);

        var r = call.ExecuteReader(@"SELECT * FROM z_user", new());
        while (r.Read())
        {
            rows.Add(new UserRow(r));
        }

        return rows;
    }

    public UserRow? GetById(long userId)
    {
        var call = new SqlDatabaseCall(_connection);

        DbDataReader r = call.ExecuteReader(
            @"SELECT * FROM z_user WHERE user_id = @user_id",
            new Dictionary<string, object> {
                {"@user_id", userId},
            });

        Dictionary<string, object> row = new Dictionary<string, object>();
        if (r.Read())
        {
            return new UserRow(r);
        }

        return null;
    }

    public UserRow? GetByEmail(string email)
    {
        var call = new SqlDatabaseCall(_connection);

        DbDataReader r = call.ExecuteReader(
            @"SELECT * FROM z_user WHERE email = @email",
            new Dictionary<string, object> {
                {"@email", email},
            });

        if (r.Read())
        {
            return new UserRow(r);
        }

        return null;
    }

    public UserRow? GetByUsername(string username)
    {
        var db = new SqlDatabaseCall(_connection);

        var r = db.ExecuteReader(
            @"SELECT * FROM z_user WHERE username = @username",
            new Dictionary<string, object> {
                {"@username", username},
            });

        if (r.Read())
        {
            return new UserRow(r);
        }

        return null;
    }

    public UserRow Insert(UserRow row)
    {
        var call = new SqlDatabaseCall(_connection);

        try
        {
            var r = call.ExecuteScalar(
                @"INSERT INTO z_user(username, email, password)
                          VALUES (@username, @email, @password)
                          RETURNING user_id",
                new Dictionary<string, object>() {
                    {"@username", row.Username},
                    {"@email", row.Email},
                    {"@password", row.Password},
                }
            );

            if (r is not null)
            {
                row.UserId = (long)r;
                return row;
            }
        }
        catch (SqliteException e)
        {
            throw;
        }

        throw new InvalidOperationException("Unable to insert new user");
    }

    public UserRow? Update(UserRow row)
    {
        var call = new SqlDatabaseCall(_connection);

        var r = call.ExecuteScalar(
            @"UPDATE z_user
              SET
                username = @username,
                email = @email,
                password = @password
              WHERE user_id = @id
              RETURNING user_id",
            new Dictionary<string, object>() {
                {"@username", row.Username},
                {"@email", row.Email},
                {"@password", row.Password},
            });

        if (r is not null) {
            return row;
        }

        return null;
    }

    public bool Delete(long userId)
    {
        var db = new SqlDatabaseCall(_connection);

        string sql = @"
            DELETE FROM z_user
            WHERE user_id = @user_id
            ";

        Dictionary<string, object> ps = new Dictionary<string, object>() {
            {"@user_id", userId},
        };

        if (db.ExecuteNonQuery(sql, ps) > 0)
        {
            return true;
        }

        return false;
    }

    private readonly DbConnection _connection;
}
