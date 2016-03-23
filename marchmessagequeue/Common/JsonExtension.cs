using System;
using Newtonsoft.Json;

namespace MarchMessageQueue.Common
{
    public static class JsonExtension
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static object ToObject(this string json, Type objType)
        {
            return JsonConvert.DeserializeObject(json, objType);
        }
        public static T ToObject<T>(this string json)
            where T : new()
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

    }
}