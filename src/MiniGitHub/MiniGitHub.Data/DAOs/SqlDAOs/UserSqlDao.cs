using System.Data;
using System.Data.Common;
using MiniGitHub.Data.Rows;
using static MiniGitHub.Data.Extensions.DbCommandExtensions;

namespace MiniGitHub.Data.DataAccessObjects;

public class UserSqlDao : IUserDao {
    public UserSqlDao(string connectionString) {
        _connectionString = connectionString;
    }

    public List<UserRow> GetAll() {
        List<UserRow> rows = new();
        using (SqlDatabaseCall db = new SqlDatabaseCall(_connectionString)) {
            var r = db.ExecuteReader(@"SELECT * FROM z_user", new());
            while (r.Read()) {
                rows.Add(new UserRow(r)); 
            }
        }
        return rows;
    }

    public UserRow? GetById(int userId) {
        using (SqlDatabaseCall db = new SqlDatabaseCall(_connectionString)) {
            DbDataReader r = db.ExecuteReader(
                @"SELECT * FROM z_user WHERE user_id = @user_id", 
                new Dictionary<string, object> {
                    {"@user_id", userId},
                });

            Dictionary<string, object> row = new Dictionary<string, object>();
            if (r.Read()) {
                return new UserRow(r);
            }

            return null;
        }
    }

    public UserRow? GetByEmail(string email) {
        using (SqlDatabaseCall db = new SqlDatabaseCall(_connectionString)) {
            DbDataReader r = db.ExecuteReader(
                @"SELECT * FROM z_user WHERE email = @email", 
                new Dictionary<string, object> {
                    {"@email", email},
                });

            if (r.Read()) {
                return new UserRow(r);
            }
        }

        return null;
    }

    public UserRow GetByUsername(string username) {
        using (SqlDatabaseCall db = new SqlDatabaseCall(_connectionString)) {
            DbDataReader r = db.ExecuteReader(
                @"SELECT * FROM z_user WHERE username = @username", 
                new Dictionary<string, object> {
                    {"@username", username},
                });

            if (r.Read()) {
                return new UserRow(r);
            }
        }

        return null;
    }

    public UserRow Insert(UserRow row) {
        using (SqlDatabaseCall db = new SqlDatabaseCall(_connectionString)) {
            var r = db.ExecuteScalar(
                @"INSERT INTO z_user(username, email, password) 
                      VALUES (@username, @email, @password) 
                      RETURNING user_id",
                new Dictionary<string, object>() {
                    {"@username", row.Username},
                    {"@email", row.Email},
                    {"@password", row.Password},
                }
            );

            if (r is not null) {
                row.UserId = (int)r;
                return row;
            }
        }

        throw new InvalidOperationException("Unable to insert new user");
    }

    public UserRow Update(UserRow row) {
        using (SqlDatabaseCall db = new SqlDatabaseCall(_connectionString)) {
            var r = db.ExecuteScalar(
                    @"UPDATE z_user 
                      SET username = @username, email = @email, password = @password 
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
        }

        throw new InvalidOperationException("Unable to update");
    }

    public bool Delete(int userId) {
        using (SqlDatabaseCall db = new SqlDatabaseCall(_connectionString)) {
            string sql = @"DELETE z_user WHERE user_id = @user_id";
            Dictionary<string, object> ps = new Dictionary<string, object>() {
                {"@user_id", userId},
            };
            if (db.ExecuteNonQuery(sql, ps) > 0) {
                return true;
            }
        }
        return false;
    }

    private readonly string _connectionString;
}