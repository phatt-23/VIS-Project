using System.Data.Common;

namespace MiniGitHub.Data.Extensions;

public static class DbDataReaderExtensions {
    public static T Get<T>(this DbDataReader r, string name) {
        int ord = r.GetOrdinal(name);
        Type t = typeof(T);
        if (t == typeof(int))
            return (T)(object)r.GetInt32(ord);
        if (t == typeof(long))
            return (T)(object)r.GetInt64(ord);
        if (t == typeof(string))
            return (T)(object)r.GetString(ord);
        if (t == typeof(DateTime))
            return (T)(object)r.GetDateTime(ord);
        if (t == typeof(bool))
            return (T)(object)r.GetBoolean(ord);
        throw new InvalidOperationException("Unsupported type.");
    }
}