using System;
using System.Collections.Generic;
using System.Text;

using xSQL.Schema.Core;
using xSQL.Schema.SqlServer;
using xSQL.SchemaCompare.SqlServer;


namespace xSQL.Sdk.SchemaCompare.Examples
{
    class Scripting
    {
        /// <summary>
        /// This method reads the schema of the database AdventureWorks and scripts the table Employee.
        /// </summary>
        public static void Script()
        {
            SqlServer server;
            SqlDatabase database;
            SqlTable table;
            ScriptingOptions options;

            try
            {
                //--create the SQL Server object
                server = new SqlServer(@"(local)");

                //--create the database object
                database = server.GetDatabase("AdventureWorks");

                //--attach an handler to database.SchemaOperation even in order to get progress information during the schema read
                database.SchemaOperation += new EventHandler<SchemaOperationEventArgs>(database_SchemaOperation);

                //--read the database schema
                database.ReadSchema();

                //--create scripting options; 
                options = new ScriptingOptions();
                options.CreateScript = true;
                options.DropScript = false;
                options.AlterScript = false;


                //--locate and script the Employee table
                table = database.SqlTables["HumanResources", "Employee"];
                if (table != null)
                    Console.Write(table.GetScript(options));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void database_SchemaOperation(object sender, SchemaOperationEventArgs e)
        {
            //--exclude verbose messages
            if (e.Message.MessageType != OperationMessageTypeEnum.Verbose)
                Console.WriteLine(e.Message.Text);
        }


    }
}
