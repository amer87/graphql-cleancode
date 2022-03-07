using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;

namespace Com.Application.Common.Utilities;

public static class ExtensionMethods
{
    public static T DeepClone<T>(this T source)
    {
        if (source == null)
        {
            return default;
        }

        var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };
        return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source), deserializeSettings);
    }

    public static T OverwriteProperties<T>(this T entity, List<KeyValuePair<string, object>> newProperties)
    {
        // TODO : Change this to be guards
        if (entity == null)
        {
            return default;
        }

        foreach (var p in newProperties)
        {
            PropertyInfo prop = entity.GetType().GetProperty(p.Key);
            prop.SetValue(entity, p.Value);
        }

        return entity;
    }
}
