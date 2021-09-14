using System;
using System.Collections.Generic;
using System.Text;

using xSQL.Schema.Core;
using xSQL.Schema.SqlServer;
using xSQL.SchemaCompare.SqlServer;

namespace xSQL.Sdk.SchemaCompare.Examples
{
    class SimpleCompare
    {
        /// <summary>
        /// This example demonstrates a typical database comparison scenario.
        /// </summary>
        public static void CompareDatabases()
        {
            SqlServer xServer, yServer;
            SqlDatabase xDatabase, yDatabase;
            SqlSchemaCompare comparer;
            ScriptManager sqlScript;
            ScriptExecutionStatusEnum status;

            try
            {

                // create the left SQL Server object using Windows authentication
                xServer = new SqlServer(@"(local)");

                // create the right SQL Server using SQL Server authentication
                yServer = new SqlServer(@"(local)", "<user>", "<password>");

                // create the left database
                xDatabase = xServer.GetDatabase("Source");

                // create the right database
                yDatabase = yServer.GetDatabase("Target");

                // create the schema comparer
                comparer = new SqlSchemaCompare(xDatabase, yDatabase);

                // exclude some database objects
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

                // step 3: compare the database schema
                comparer.Compare();

                // check for errors that could have occurred during the schema compare.
                // some errors are handled quietly and do not stop the process; those that are critical throw exceptions
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
                // on Target database to make it the same as the Source database
                sqlScript = comparer.GetRightDatabaseScript();

                if (!sqlScript.IsEmpty())
                {
                    // print the synchronization log
                    Console.Write(sqlScript.GetLog());

                    // print the synchronization script
                    Console.Write(sqlScript.GetScript());


                    // attach an event handler to ScriptManager object to get some progress info during the script execution
                    sqlScript.SchemaScriptExecuting += new EventHandler<SchemaScriptEventArgs>(sqlScript_SchemaScriptExecuting);

                    // execute the sync script
                    status = sqlScript.Execute();

                    // check the execution and print any errors
                    if (status == ScriptExecutionStatusEnum.Succeeded)
                    {
                        Console.WriteLine("Database synchronization finished successfully");
                    }
                    else if (status == ScriptExecutionStatusEnum.Canceled)
                    {
                        Console.WriteLine("Database synchronization was canceled");
                    }
                    else
                    {
                        // check for quite errors
                        if (ErrorRepository.Instance.HasErrors())
                        {
                            Console.WriteLine("Some errors occurred during the script execution");
                            Console.Write(ErrorRepository.Instance.GetErrors());
                        }
                    }
                }
            }
            catch (ConnectionException ex)
            {
                // a connection exception
                Console.Write(ex.ToString());
            }
            catch (SchemaException ex)
            {
                // a schema-read exception
                Console.Write(ex.ToString());
            }
            catch (SchemaCompareException ex)
            {
                // a schema compare exception
                Console.Write(ex.ToString());
            }
            catch (ScriptExecutionException ex)
            {
                // a script execution exception
                Console.Write(ex.ToString());
                Console.WriteLine("Script fragments that failed:");
                foreach (string err in ex.Errors)
                    Console.WriteLine(err);
            }
            catch (Exception ex)
            {
                // a generic exception
                Console.WriteLine("An unexpected error occurred.");
                Console.Write(ex.Message);
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
