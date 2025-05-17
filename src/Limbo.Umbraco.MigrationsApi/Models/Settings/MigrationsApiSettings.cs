namespace Limbo.Umbraco.MigrationsApi.Models.Settings;

public class MigrationsApiSettings {

    public string ApiKey { get; set; } = string.Empty;

    public HashSet<string> AllowList { get; set; } = [];

    public MigrationsApiUserSettings Users { get; set; } = new();

}