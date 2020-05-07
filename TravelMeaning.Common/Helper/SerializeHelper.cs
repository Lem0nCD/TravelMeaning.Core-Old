using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TravelMeaning.Common.Helper
{
    public class SerializeHelper
    {
        public static byte[] Serialize(object item)
        {
            var jsonString = JsonConvert.SerializeObject(item);
            return Encoding.UTF8.GetBytes(jsonString);
        }

        public static T Deserialize<T>(byte[] value)
        {
            if (value == null)
            {
                return default(T);
            }
            var jsonString = Encoding.UTF8.GetString(value);
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
