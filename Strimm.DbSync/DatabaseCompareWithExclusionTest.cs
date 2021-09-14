using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using xSQL.Schema.Core;
using xSQL.Schema.SqlServer;
using xSQL.SchemaCompare.SqlServer;


namespace xSQL.Sdk.SchemaCompare.Examples
{
    public class DatabaseCompareWithExclusionTest
    {
        /// <summary>
        /// This examples excludes some database objects via schema filters
        /// </summary>
        /// <remarks>
        /// Schema filters have been depreciated as of version 5.0 and replaced by the entity filters. 
        /// </remarks>
        public static void CompareWithSchemaFiltersTest()
        {
            SqlServer server;
            SqlServer server2;
            SqlDatabase xDatabase, yDatabase;
            SqlSchemaCompare comparer;
            ScriptManager sqlScript;
            ScriptExecutionStatusEnum status;
            SchemaFilter filter;
            SchemaFilter filter2;

            try
            {
                // create the SQL Server object
                //server = new SqlServer("strimmdb.cvpybmbtb7w8.us-east-1.rds.amazonaws.com", "stdbrdsadmin", "nuSZTzYuMH");
                server = new SqlServer("tfs2013.strimm.com", "stdbrdsadmin", "nuSZTzYuMH");
                server2 = new SqlServer("tfs2013.strimm.com", "stdbrdsadmin", "nuSZTzYuMH");

                // create the left database
                xDatabase = server.GetDatabase(@"DB_STubeDEV");

                // create the right database
                yDatabase = server2.GetDatabase(@"DB_STubeQA");

                // create the comparer
                comparer = new SqlSchemaCompare(xDatabase, yDatabase);

                comparer.Options.CompareSchemas = true;
                comparer.Options.CompareSequences = true;
                comparer.Options.CompareTables = true;
                comparer.Options.CompareTriggers = true;
                comparer.Options.CompareViews = true;
                comparer.Options.CompareUsers = false;
                comparer.Options.CompareDatabaseRoles = false;
                comparer.Options.CompareIndexes = true;
                comparer.Options.CompareApplicationRoles = false;

                var filterSchema = new SchemaFilter(SqlEntityTypeEnum.Schema);
                filterSchema.SchemaFilterCriteria.Add(new SchemaFilterCriteria("dbo", SchemaFilterTypeEnum.Schema, false));
                filterSchema.SchemaFilterCriteria.Add(new SchemaFilterCriteria("strimm", SchemaFilterTypeEnum.Schema, true));
                filterSchema.SchemaFilterCriteria.Add(new SchemaFilterCriteria("aspnet_", SchemaFilterTypeEnum.Contains, false));
                comparer.SchemaFilters.Add(filterSchema);

                // create a filter that excludes all tables starting with "tmp"
                var filterTable = new SchemaFilter(SqlEntityTypeEnum.Table);
                filterTable.SchemaFilterCriteria.Add(new SchemaFilterCriteria("dbo", SchemaFilterTypeEnum.Schema, false));
                filterTable.SchemaFilterCriteria.Add(new SchemaFilterCriteria("strimm", SchemaFilterTypeEnum.Schema, true));
                filterTable.SchemaFilterCriteria.Add(new SchemaFilterCriteria("aspnet_", SchemaFilterTypeEnum.Contains, false));
                comparer.SchemaFilters.Add(filterTable);

                var filterSprocs = new SchemaFilter(SqlEntityTypeEnum.StoredProcedure);
                filterSprocs.SchemaFilterCriteria.Add(new SchemaFilterCriteria("dbo", SchemaFilterTypeEnum.Schema, false));
                filterSprocs.SchemaFilterCriteria.Add(new SchemaFilterCriteria("strimm", SchemaFilterTypeEnum.Schema, true));
                comparer.SchemaFilters.Add(filterSprocs);

                var filterViews = new SchemaFilter(SqlEntityTypeEnum.View);
                filterViews.SchemaFilterCriteria.Add(new SchemaFilterCriteria("dbo", SchemaFilterTypeEnum.Schema, false));
                filterViews.SchemaFilterCriteria.Add(new SchemaFilterCriteria("strimm", SchemaFilterTypeEnum.Schema, true));
                comparer.SchemaFilters.Add(filterViews);

                var filterIndexes = new SchemaFilter(SqlEntityTypeEnum.Index);
                filterIndexes.SchemaFilterCriteria.Add(new SchemaFilterCriteria("dbo", SchemaFilterTypeEnum.Schema, false));
                filterIndexes.SchemaFilterCriteria.Add(new SchemaFilterCriteria("strimm", SchemaFilterTypeEnum.Schema, true));
                comparer.SchemaFilters.Add(filterIndexes);

                var filterTriggers = new SchemaFilter(SqlEntityTypeEnum.Trigger);
                filterTriggers.SchemaFilterCriteria.Add(new SchemaFilterCriteria("rds_", SchemaFilterTypeEnum.StartsWith, false));
                comparer.SchemaFilters.Add(filterTriggers);

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

                var cleanScriptCollection = new SchemaScriptFragmentCollection();

                sqlScript.TransactionalScripts.ForEach(x =>
                {
                    var transScript = x.GetScript();
                    if (!transScript.Contains("[dbo]") && !transScript.Contains("rds_") && 
                        !transScript.Contains("aspnet_") && !transScript.Contains("dbo.") &&
                        !transScript.Contains("vw_aspnet_") && !transScript.Contains("ALTER AUTHORIZATION ON SCHEMA"))
                    {
                        cleanScriptCollection.Add(x);
                    }
                });

                sqlScript.TransactionalScripts.Clear();
                sqlScript.TransactionalScripts.AddRange(cleanScriptCollection);

                var sq = sqlScript.GetScript();

                if (!sqlScript.IsEmpty())
                {
                    // use this event to get some progress during the script execution
                    sqlScript.SchemaScriptExecuting += new EventHandler<SchemaScriptEventArgs>(sqlScript_SchemaScriptExecuting);

                    // create snapshot of the right database prior to updating it.
                    DirectoryInfo dir = new DirectoryInfo(@"C:\temp\snapshots");
                    if (!dir.Exists) {
                        dir.Create();
                    }

                    yDatabase.SaveToSnapshot(Path.Combine(dir.FullName, String.Format("DB_Snapshot_ST_StrimmQA_{0}.sql",DateTime.Now.ToString())));

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
                Console.Write(ex.ToString());
            }

        }

        /// <summary>
        /// This examples excludes the Employees table from the comparison.
        /// </summary>
        public static void CompareWithExclusion()
        {
            SqlServer server;
            SqlDatabase xDatabase, yDatabase;
            SqlSchemaCompare comparer;
            ScriptManager sqlScript;
            ScriptExecutionStatusEnum status;
            SqlTablePair pair;

            try
            {
                // create the SQL Server object
                server = new SqlServer(@"(local)");

                // create the left database
                xDatabase = server.GetDatabase("Source", "Data Source=strimmdb.cvpybmbtb7w8.us-east-1.rds.amazonaws.com;Initial Catalog=StrimmWMonoX;Persist Security Info=True;User ID=stdbrdsadmin;Password=nuSZTzYuMH;");

                // create the right database
                yDatabase = server.GetDatabase("Target", "Data Source=qa.strimm.com;Initial Catalog=QADB;Persist Security Info=True;User ID=VYagRSQL;Password=Mirts05!!!");

                // create the comparer
                comparer = new SqlSchemaCompare(xDatabase, yDatabase);


                // attach event handlers to these events in order to get some progress information during the schema read and compare
                comparer.LeftDatabase.SchemaOperation += new EventHandler<SchemaOperationEventArgs>(database_SchemaOperation);
                comparer.RightDatabase.SchemaOperation += new EventHandler<SchemaOperationEventArgs>(database_SchemaOperation);
                comparer.SchemaOperation += new EventHandler<SchemaOperationEventArgs>(database_SchemaOperation);

                // use the generic event EntityPairingFinished to exclude an object from the comparison or
                // the SqlTablePairingFinished event which is raised specifically for database tables.
                // the table-specific event performs slightly better since it has to check only tables and not every object in the database
                // objects are usually excluded after the pairing is finished. 
                comparer.SqlTablePairingFinished += new EventHandler<SqlTablePairEventArgs>(comparer_SqlTablePairingFinished);


                // step 1: read the schema 
                comparer.ReadSchema();

                // step 2: pair the database objects
                comparer.PairObjects();

                // this is another way of an excluding an object; it should be done always after the pairing finishes
                pair = comparer.SqlTablePairs["employees"];
                if (pair != null)
                    pair.Included = false;


                // step 3: compare the schema
                comparer.Compare();

                // check for errors that could have occurred during the schema compare. 
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

        private static void comparer_SqlTablePairingFinished(object sender, SqlTablePairEventArgs e)
        {
            if (e.Pair.ContainsMember("employees", SqlEntityTypeEnum.Table))
                e.Pair.Included = false;
        }
    }
}
