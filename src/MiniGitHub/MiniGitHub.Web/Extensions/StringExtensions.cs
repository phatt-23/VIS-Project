namespace MiniGitHub.Web.Extensions;

public static class StringExtensions {
    
    public static int ToInt(this string self) {
        return int.Parse(self);
    }
    
    public static long ToLong(this string self) {
        return long.Parse(self);
    }
    
    public static int? TryToInt(this string self)
    {
        if (int.TryParse(self, out int result)) {
            return result;
        }
        
        return null;
    }
    
    public static long? TryToLong(this string self) {
        if (long.TryParse(self, out long result)) {
            return result;
        }

        return null;
    }

    public static string OrDefault(this string? self, string defaultValue) {
        return self ?? defaultValue;
    } 
}