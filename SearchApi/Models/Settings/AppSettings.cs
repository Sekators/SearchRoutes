namespace SearchApi.Models.Settings;

public class AppSettings
{
    public int CacheExpirationMinutes { get; set; }
    public int TimeoutSeconds { get; set; }
}