using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
public class BaseStartupConfig
{
    public static IConfiguration Configuration { get; set; }
    public static void InitConfig()
    {
        if(Configuration == null)
        {
            try
            {
                var env = Environment.CurrentDirectory;
                if(env.IndexOf(@"\backend\") > -1)
                {
                    env = env.Substring(0, env.IndexOf(@"\backend\") + (@"\backend\".Length - 1));
                }
           
                Configuration = new ConfigurationBuilder()
                                .SetBasePath(env + "/config")
                                .AddJsonFile("appsettings.json")
                                .Build();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public static void ConfigureServices(ref IServiceCollection services)
    {
        InitConfig();
        services.AddDistributedMemoryCache();
        services.AddSession(options => {
            options.IdleTimeout = TimeSpan.FromMinutes(5);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
        services.AddCors();

        services.Configure<GlobalConfig>(Configuration.GetSection("AppSettings"));
    }

}
