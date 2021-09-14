using System;
using System.Collections.Generic;
using System.Text;

using xSQL.Schema.Core;
using xSQL.Schema.SqlServer;
using xSQL.SchemaCompare.SqlServer;

namespace xSQL.Sdk.SchemaCompare.Examples
{
    class DatabaseSync
    {

        /// <summary>
        /// Compares two databases, generates the synchronization script and execute on the Target database.
        /// </summary>
        public static void Synchronize()
        {
            SqlServer server;
            SqlDatabase xDatabase, yDatabase;
            SqlSchemaCompare comparer;
            ScriptManager sqlScript;
            ScriptExecutionStatusEnum status;

            try
            {
                // create the SQL Server object; both databases are located in the same SQL Server
                server = new SqlServer(@"(local)");

                // create the left database
                xDatabase = server.GetDatabase("Source");

                // create the right database
                yDatabase = server.GetDatabase("Target");


                // create the comparer
                comparer = new SqlSchemaCompare(xDatabase, yDatabase);


                // exclude some objects from the comparison
                comparer.Options.CompareUsers = false;
                comparer.Options.CompareSchemas = false;
                comparer.Options.CompareDatabaseRoles = false;
                comparer.Options.CompareApplicationRoles = false;


                // attach event handlers to these events in order to get some progress information during the schema read and compare
                comparer.LeftDatabase.SchemaOperation += new EventHandler<SchemaOperationEventArgs>(database_SchemaOperation);
                comparer.RightDatabase.SchemaOperation += new EventHandler<SchemaOperationEventArgs>(database_SchemaOperation);
                comparer.SchemaOperation += new EventHandler<SchemaOperationEventArgs>(database_SchemaOperation);

                // step 1: read the schema 
                comparer.ReadSchema();

                // step 2: pair the database objects
                comparer.PairObjects();

                // step 3: compare the schema
                comparer.Compare();

                // check for errors that could have occurred during the schema compare. 
                // some errors are handled quietly and do not stop the process, those that are critical throw exceptions
                // quiet errors are collected and stored into the ErrorRepository object
                if (ErrorRepository.Instance.HasErrors())
                {
                    Console.WriteLine("Some errors occurred during the database compare");
                    Console.Write(ErrorRepository.Instance.GetErrors());
                }

                // check the database status; exit if no schema differences are found.
                if (comparer.SqlDatabasePair.ComparisonStatus == ComparisonStatusEnum.Equal)
                    return;


                // step 4: get the T-SQL script intended for the right database; that is the script that should be executed 
                // on Target database in order to make it the same as the Source database
                sqlScript = comparer.GetRightDatabaseScript();

                if (!sqlScript.IsEmpty())
                {
                    // use this event to get some progress during the script execution
                    sqlScript.SchemaScriptExecuting += new EventHandler<SchemaScriptEventArgs>(sqlScript_SchemaScriptExecuting);

                    // execute the synchronization script 
                    status = sqlScript.Execute();

                    // check the status
                    if (status == ScriptExecutionStatusEnum.Succeeded)
                    {
                        Console.WriteLine("Database synchronization succeeded.");
                    }
                    else if (status == ScriptExecutionStatusEnum.Canceled)
                    {
                        Console.WriteLine("Database synchronization was canceled.");
                    }
                    else
                    {
                        //--check for silent errors
                        if (ErrorRepository.Instance.HasErrors())
                        {
                            Console.WriteLine("Some errors occurred during the script execution.");
                            Console.Write(ErrorRepository.Instance.GetErrors());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database synchronization failed.");
                Console.Write(ex.ToString());
            }


        }

        private static void database_SchemaOperation(object sender, SchemaOperationEventArgs e)
        {
            //--exclude verbose messages
            if (e.Message.MessageType != OperationMessageTypeEnum.Verbose)
                Console.WriteLine(e.Message.Text);
        }

        private static void sqlScript_SchemaScriptExecuting(object sender, SchemaScriptEventArgs e)
        {
            Console.WriteLine("{0} {1}", DateTime.Now.ToString("HH:mm:ss"), e.Script);
        }

    }

}
