using Nest;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;

namespace NDQUAN.Core.Common
{
    public class LogManager
    {
        public static IndexResponse LogES()
        {
            try
            {
                var redisConnection = ConfigUtil.GetGlobalConfig().RedisConnection.ConnectionString;
                var settings = new ConnectionSettings(new Uri(redisConnection))
                            .DefaultIndex("personindex");

                var client = new ElasticClient(settings);
                var person = new PersonIndex
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Martijn",
                    LastName = "Laarman"
                };
                var idxRes = client.IndexDocument(person);
                return idxRes;
            }
            catch (Exception)
            {

            }
            return null;
        }
    }

    public class PersonIndex
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
