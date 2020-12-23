using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingApp.Models {
    public enum RolType {
        editor,
        user,
        anonymousUser
    }

    public static class RolTypeExtencions {
        public static string GetDescription(this RolType RolType) {
            switch(RolType) {
                case RolType.editor:
                    return "User that approves publication and publish them.";
                    break;
                case RolType.user:
                    return "A registered user.";
                    break;
                case RolType.anonymousUser:
                    return "A non registered user that can publish a blog.";
                    break;
            }

            return "No description available.";
        }
    }
}
