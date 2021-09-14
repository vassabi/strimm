using System;
using System.Collections.Generic;
using System.Text;

using xSQL.Schema.Core;
using xSQL.Schema.SqlServer;
using xSQL.SchemaCompare.SqlServer;


namespace xSQL.Sdk.SchemaCompare.Examples
{
    class DatabaseCompare
    {
        /// <summary>
        /// This example demonstrates a database comparison. The synchronization script is generated but not executed.
        /// </summary>
        public static void Compare()
        {
            SqlServer xServer, yServer;
            SqlDatabase xDatabase, yDatabase;
            SqlSchemaCompare comparer;
            ScriptManager sqlScript;

            try
            {

                // create the SQL Server objects 
                xServer = new SqlServer(@"LeftServer");
                yServer = new SqlServer(@"RightServer");

                // create the left database
                xDatabase = xServer.GetDatabase("Source");

                // create the right database
                yDatabase = yServer.GetDatabase("Target");

                // create the schema comparer
                comparer = new SqlSchemaCompare(xDatabase, yDatabase);


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

                // print the synchronization log
                Console.Write(sqlScript.GetLog());

                // print the synchronization script
                Console.Write(sqlScript.GetScript());

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
    }
}
