using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingApp.Models {
    public enum BlogStatus {
        _none,
        posted,
        pendingPublishApproval,
        publicated,
        deleted,
        rejected
    }
}
