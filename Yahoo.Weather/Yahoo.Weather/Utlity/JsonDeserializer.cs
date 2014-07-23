using System;

namespace WeatherInformation.Utlity
{
    class JsonDeserializer<T> where T : class, new()
    {
           public T DeSerialize(string jsonResponse)
            {
                if (jsonResponse.IsNotNullOrEmpty())
                {
                    try
                    {
                        var deserializeResponse = jsonResponse.JsonDeserialize<T>();

                        return deserializeResponse;
                    }
                    catch (Exception ex)
                    {
                        var logger = new ErrorLog() ;

                        string info = string.Format("<br/>Json : {0}<br/>class name : {1}", jsonResponse, typeof(T));

                        logger.LogError(ex, info);
                    }
                }

                return null;
            }
        }
    public static class NewtonsoftJsonExtension
    {
        public static T JsonDeserialize<T>(this string json) where T : class, new()
        {
            if (json == null) throw new ArgumentNullException("json");

            return (T)Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof(T));
        }
    }
    
}
