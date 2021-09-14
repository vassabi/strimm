using RedGate.SQLCompare.Engine.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedGate.SQLCompare.Engine;
using Strimm.SqlCompare.Enums;

namespace Strimm.SqlCompare.Extensions
{
    public static class DifferenceFilterExtensions
    {
        /// <summary>
        /// This extension method will set include filter object types based on the filter options
        /// specified by end user
        /// </summary>
        /// <param name="filter">Instance of different filter</param>
        /// <param name="filterOptions">User include filter options</param>
        public static void SetFilterObjectTypeIncludesFromOptions(this DifferenceFilter filter, List<KeyValuePair<DiffFilterType, string>> filterOptions)
        {
            if (filterOptions != null && filterOptions.Count > 0)
            {
                filterOptions.ForEach(x =>
                {
                    switch (x.Key)
                    {
                        case DiffFilterType.Function:
                            filter.SetObjectTypeInclude(ObjectType.Function);
                            break;
                        case DiffFilterType.Schema:
                            filter.SetObjectTypeInclude(ObjectType.Schema);
                            break;
                        case DiffFilterType.StoredProcedure:
                            filter.SetObjectTypeInclude(ObjectType.StoredProcedure);
                            break;
                        case DiffFilterType.Table:
                            filter.SetObjectTypeInclude(ObjectType.Table);
                            break;
                        case DiffFilterType.UserDefinedType:
                            filter.SetObjectTypeInclude(ObjectType.UserDefinedType);
                            break;
                        case DiffFilterType.View:
                            filter.SetObjectTypeInclude(ObjectType.View);
                            break;
                        default:
                            break;
                    }
                });

            }
            else
            {
                filter.SetObjectTypeInclude(ObjectType.Table);
            }
        }
    }
}
