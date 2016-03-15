using Newtonsoft.Json;

namespace MarchMessageQueue.Common
{
    public static class JsonExtension
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}