using Newtonsoft.Json.Linq;

namespace Com.Application.Common.Utilities;

public static class JsonTools
{
    public static string GetSingalValueFromJsonObject(string jsonObject, string valueName)
    {
        if (string.IsNullOrEmpty(jsonObject)) return "";

        JObject jObject = JObject.Parse(jsonObject);
        return (string)jObject.SelectToken(valueName);
    }
}
