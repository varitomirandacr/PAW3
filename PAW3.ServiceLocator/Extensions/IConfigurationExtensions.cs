public static class IConfigurationExtensions
{
    public static string GetStringFromAppSettings(this IConfiguration configuration, string section, string key)
    {
        string final = "";
        var apiSection = configuration.GetSection(section).Get<List<Dictionary<string, string>>>();
        apiSection?.ForEach(x =>
        {
            var value = x.TryGetValue(key, out var valueString);
            if (valueString != null)
            {
                final = valueString;
                return;
            }
        });
        return final;
    }

}
