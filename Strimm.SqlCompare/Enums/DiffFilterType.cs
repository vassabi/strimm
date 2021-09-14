using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.SqlCompare.Enums
{
    public enum DiffFilterType
    {
        Function,
        Index,
        Schema,
        Data,
        StoredProcedure,
        Table,
        Trigger,
        UserDefinedType,
        View
    }
}
