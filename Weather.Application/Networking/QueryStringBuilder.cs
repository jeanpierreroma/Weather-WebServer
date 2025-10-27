using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace Weather.Application.Networking;

public static class QueryStringBuilder
{
    public static string Append(string basePath, object request)
    {
        var pairs = ToPairs(request).ToArray();
        if (pairs.Length == 0) return basePath;

        var sb = new StringBuilder(basePath);
        sb.Append(basePath.Contains('?') ? '&' : '?');

        for (int i = 0; i < pairs.Length; i++)
        {
            var (k, v) = pairs[i];
            if (i > 0) sb.Append('&');
            sb.Append(Uri.EscapeDataString(k));
            sb.Append('=');
            sb.Append(Uri.EscapeDataString(v));
        }
        return sb.ToString();
    }

    private static IEnumerable<(string Key, string Value)> ToPairs(object obj)
    {
        foreach (var p in obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
            if (p.GetCustomAttribute<JsonIgnoreAttribute>() is not null) continue;

            var name = p.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? p.Name;
            var val  = p.GetValue(obj);
            if (val is null) continue;

            switch (val)
            {
                case string s when !string.IsNullOrWhiteSpace(s):
                    yield return (name, s);
                    break;

                // масиви/колекції рядків — у форматі "a,b,c"
                case IEnumerable<string> ss:
                    var list = ss.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                    if (list.Length > 0) yield return (name, string.Join(",", list));
                    break;

                // вкладені об'єкти — розкладаємо рекурсивно
                case object o when !IsSimple(p.PropertyType):
                    foreach (var kv in ToPairs(o)) yield return kv;
                    break;

                // числа, дати тощо — InvariantCulture
                case IFormattable f:
                    yield return (name, f.ToString(null, CultureInfo.InvariantCulture)!);
                    break;

                default:
                    yield return (name, val.ToString()!);
                    break;
            }
        }
    }

    private static bool IsSimple(Type t)
    {
        t = Nullable.GetUnderlyingType(t) ?? t;
        return t.IsPrimitive || t.IsEnum || t == typeof(string) ||
               t == typeof(decimal) || t == typeof(double) || t == typeof(float) ||
               t == typeof(DateTime) || t == typeof(Guid);
    }
}