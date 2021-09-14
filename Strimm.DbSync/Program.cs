using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using xSQL.Licensing;
using xSQL.Licensing.SqlServer;

namespace xSQL.Sdk.SchemaCompare.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlLicenseStore.CreateInstance(new SqlSchemaSdkProduct());
            SqlLicenseStore.Instance.RegisterLicense("license_number", "hash_value");

            //DatabaseCompare.Compare();
            DatabaseCompareWithExclusionTest.CompareWithSchemaFiltersTest();
            Console.ReadKey();
        }
    }
}
