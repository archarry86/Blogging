
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BloggingApp.Helpers {
    public static class SessionHelper {
        public static void  SetObject(this ISession session, string Key, object Value) {
            session.SetString(Key, JsonSerializer.Serialize(Value));
        }

        public static T GetObject<T>(this ISession session, string Key) {
            //here I could have used a try catch bloc in order to use an own exception
            T t = default(T);
            var jsonString = session.GetString(Key);
            if(!String.IsNullOrEmpty( jsonString))
            t = JsonSerializer.Deserialize<T>(jsonString);
           
            return t;
        }
    }
}
