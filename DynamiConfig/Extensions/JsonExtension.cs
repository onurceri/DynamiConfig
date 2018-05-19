using ServiceStack;

namespace DynamiConfig.Extensions
{
    public static class JsonExtension
    {
        public static string ToRedisJson(this object convertObject)
        {
            return convertObject.ToJson();
        }
        public static T FromJsonToObject<T>(this string convertObject)
        {
            return convertObject.FromJson<T>();
        }
    }
}
