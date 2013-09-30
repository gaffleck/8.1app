using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace DAL
{
    public static class Extensions
    {


        public static object Deserialize(this string json, Type type)
        {
            var _Bytes = Encoding.Unicode.GetBytes(json);
            using (MemoryStream _Stream = new MemoryStream(_Bytes))
            {
                var _Serializer = new DataContractJsonSerializer(type);
                return _Serializer.ReadObject(_Stream);
            }
        }

        public static IDictionary<string,object> Serialize(this object instance, Type type)
        {
            var ret = new Dictionary<string, object>();
            foreach (var prop in type.GetRuntimeProperties())
            {
                ret.Add(prop.Name, prop.GetValue(instance));
            }
            return ret; 
        }

    }
}
