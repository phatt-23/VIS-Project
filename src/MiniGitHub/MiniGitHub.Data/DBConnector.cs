using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;

namespace MiniGitHub.Data {

public enum DBProvider {
    Sqlite, 
    MsSql,
}

public static class DBConnector {
    public static DBProvider Provider { get; set; } = DBProvider.Sqlite;
    private static bool _initialized = false;

    public static void Init() {
        if (_initialized)
            return;
        
        if (Provider == DBProvider.Sqlite)
            SQLitePCL.Batteries.Init();

        _initialized = true;
    }
    
    public static DbConnection CreateConnection() {
        Init();  
        return DBConnector.Provider switch {
            DBProvider.Sqlite => new SqliteConnection(GetConnectionString()),
            DBProvider.MsSql => throw new NotImplementedException("MsSql provider is not implemented"),
            _ => throw new InvalidOperationException("Unknown provider"),
        };
    }

    public static string GetConnectionString() {
        Init(); 
        switch (DBConnector.Provider) {
        case  DBProvider.Sqlite: 
            string solutionDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
            string dbPath = Path.Combine(solutionDir, "MiniGitHubDB.sqlite3");
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = dbPath;
            return builder.ConnectionString;
            break;
        case DBProvider.MsSql:
            throw new NotImplementedException("MsSql provider is not implemented");
            break;
        default:
            throw new InvalidOperationException("Unknown provider");
            break;
        }
    }
}

}
