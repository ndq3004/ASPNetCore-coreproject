using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

public static class ConfigUtil
{
    public static Dictionary<string, IConfiguration> _config = new Dictionary<string, IConfiguration>();
    public static GlobalConfig GetGlobalConfig()
    {
        var env = Environment.CurrentDirectory;
        if (env.IndexOf(@"\backend\") > -1)
        {
            env = env.Substring(0, env.IndexOf(@"\backend\") + (@"\backend\".Length - 1));
        }

        var configuration = new ConfigurationBuilder()
                        .SetBasePath(env + "/config")
                        .AddJsonFile("appsettings.json")
                        .Build();
        var config = configuration.GetSection("AppSettings").Get<GlobalConfig>();
        return config;
    }
}
