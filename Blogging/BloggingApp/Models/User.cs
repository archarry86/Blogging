using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BloggingApp.Models {
    public class User {
        public int Id { get;  set; }
        public string Login { get;  set; }
        [JsonIgnore]
        public string Password { get; set; }
        public Role Role { get;  set; }

    }
}
