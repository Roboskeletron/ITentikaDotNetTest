using Microsoft.Extensions.Configuration;

namespace ITentikaTest.Common.Settings;

public abstract class Settings
{
    public static T Load<T>(string key, IConfiguration? configuration = null)
    {
        var settings = (T)Activator.CreateInstance(typeof(T));
        
        ArgumentNullException.ThrowIfNull(settings);

        SettingsFactory.Create(configuration).GetSection(key)
            .Bind(settings, x => { x.BindNonPublicProperties = true; });

        return settings;
    }
}