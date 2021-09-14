using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using xSQL.Schema.Core;
using xSQL.Schema.SqlServer;
using xSQL.SchemaCompare.SqlServer;


namespace xSQL.Sdk.SchemaCompare.Examples
{
    class DatabaseSnapshot
    {
        /// <summary>
        /// Creates a snapshot for the AdventureWorks database
        /// </summary>
        public static void CreateSnapshot()
        {
            SqlServer server;
            SqlDatabase database;

            try
            {
                //--create the SQL Server object, connect using Windows authentication
                server = new SqlServer(@"(local)");

                //--create the database object
                database = server.GetDatabase("AdventureWorks");

                //--attach an handler to SchemaOperation even in order to get progress information during the schema read
                database.SchemaOperation += new EventHandler<SchemaOperationEventArgs>(database_SchemaOperation);

                //--attach an handler to SnapshotOperation event to get snapshot feedback
                database.SnapshotOperation += new EventHandler<SnapshotOperationEventArgs>(database_SnapshotOperation);

                //--read the database schema
                database.ReadSchema();

                //--create the snapshot
                database.SaveToSnapshot(Path.Combine(@"C:\", database.GetDefaultSnapshotFilename()));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Creates the AdventureWorks database from a snapshot file.
        /// </summary>
        public static void CreateDatabaseFromSnapshot()
        {
            SqlDatabase database;
            SqlTable table;
            ScriptingOptions options;

            try
            {
                //--create the database from the snapshot file
                database = SqlDatabase.CreateFromSnapshot(@"C:\AdventureWorks.snpx");

                //--script the Employee 
                options = new ScriptingOptions();
                options.CreateScript = true;
                options.DropScript = false;
                options.AlterScript = false;

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

        private static void database_SnapshotOperation(object sender, SnapshotOperationEventArgs e)
        {
            //--exclude verbose messages
            if (e.Message.MessageType != OperationMessageTypeEnum.Verbose)
                Console.WriteLine(e.Message.Text);
        }

    }
}
