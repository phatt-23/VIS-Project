using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MiniGitHub.Web.Extensions;

public static class EnumExtensions {
    public static string GetDisplayName(this Enum value) {
        MemberInfo[] member = value.GetType().GetMember(value.ToString());
        DisplayAttribute? attribute = member[0]
            .GetCustomAttributes(typeof(DisplayAttribute), false)
            .FirstOrDefault() as DisplayAttribute;

        return attribute?.Name ?? value.ToString();
    }
}