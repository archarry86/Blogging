using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingApp.Models {
    public class Role {
        public int Id { get; set; }
        public RolType RolType { get; set; }

        public override string ToString() {
            return $"Rol: {Id} {RolType} {RolType.GetDescription()}";
        }
    }
}
