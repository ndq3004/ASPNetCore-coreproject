using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Resources.NetStandard;

namespace NDQUAN.Resource
{
    public class ResourceDataManager
    {
        public static dynamic GetModelMappingName()
        {
            var dic = new Dictionary<string, object>();
            var env = Environment.CurrentDirectory;
            if (env.IndexOf(@"\backend\") > -1)
            {
                env = env.Substring(0, env.IndexOf(@"\backend\") + (@"\backend\".Length - 1));
            }
            ResourceManager rm = new ResourceManager("NDQUAN.Resource.ModelMapping", Assembly.GetExecutingAssembly());
            string res = rm.GetString("model_a");
            return dic;
        }
    }
}
