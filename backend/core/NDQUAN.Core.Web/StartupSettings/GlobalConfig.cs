using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class GlobalConfig
{
    public string Secret { get; set; }
    public string UserDatabaseConnectionString { get; set; }

    public RedisConnection RedisConnection { get; set; }
}

public class RedisConnection
{
    public string ConnectionString { get; set; }
}
