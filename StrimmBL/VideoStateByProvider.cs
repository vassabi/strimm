using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrimmBL
{
    public enum VideoStateByProvider
    {
        VIEWABLE = 0,
        DELETED = 1,
        FAILED = 2,
        REJECTED = 3,
        PRIVATE = 4,
        RESTRICTED = 5
    }
}
