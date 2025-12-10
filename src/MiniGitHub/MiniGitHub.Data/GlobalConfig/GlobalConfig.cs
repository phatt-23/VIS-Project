using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using MiniGitHub.Data.DataConnector;
using MiniGitHub.Data.DataConnector.SqlConnector;

namespace MiniGitHub.Data;

public enum DataSourceType {
    Text, 
    Sqlite,
}

public static class GlobalConfig {

    public static IDataConnector GetDataConnector() {
        if (_dataSourceType == DataSourceType.Sqlite) {
            return new SqlDataConnector();
        } 
        
        if (_dataSourceType == DataSourceType.Text) {
            return new TextDataConnector();
        }
        
        throw new InvalidOperationException("Unknown data source");
    }

    public static void SetDataSourceType(DataSourceType sourceType) {
        _dataSourceType = sourceType;
    }
    
    public static void SetDataSource(string source) {
        switch (_dataSourceType) {
        case DataSourceType.Text:
            _textDbDirectory = source;
            break;
        case DataSourceType.Sqlite:
            _sqliteFile = source;
            break;
        default:
            throw new ArgumentOutOfRangeException();
        }
    }

    // helper for getting the text file database
    public static string GetTextFilePath() {
        string solutionDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
        string dbPath = Path.Combine(solutionDir, _textDbDirectory);
        return dbPath;
    }

    // helper for creating a connection string 
    public static string GetSqlConnectionString() {
        string solutionDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
        string dbPath = Path.Combine(solutionDir, _sqliteFile);
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        builder.DataSource = dbPath;
        return builder.ConnectionString;
    }

    // private static readonly string _dataSource = "sqlite";
    private static DataSourceType _dataSourceType = DataSourceType.Text;
    
    // defaults
    private static string _sqliteFile = "MiniGitHubDB.sqlite3";
    private static string _textDbDirectory = "MiniGitHubDB.txt.data/";
}