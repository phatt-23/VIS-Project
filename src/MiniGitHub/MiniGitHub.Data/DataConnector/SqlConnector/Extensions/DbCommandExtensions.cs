using System.Data.Common;

namespace MiniGitHub.Data.DataConnector.SqlConnector.Extensions;

public static class DbCommandExtensions {
    public static void AddWithValue(this DbCommand cmd, string name, object? value) {
        DbParameter parameter = cmd.CreateParameter();
        parameter.ParameterName = name;
        parameter.Value = value;
        cmd.Parameters.Add(parameter);
    }
}