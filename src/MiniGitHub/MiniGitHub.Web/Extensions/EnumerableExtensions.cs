using System.Collections;

namespace MiniGitHub.Web.Extensions;

public static class EnumerableExtensions {
    public static IEnumerable<(int, T)> Indexed<T>(this IEnumerable<T> self) {
        return self.Select((v, index) => (index, v));
    }
}