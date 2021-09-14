using System;
using System.Collections.Generic;
using System.Text;

using xSQL.Schema.Core;
using xSQL.Schema.SqlServer;
using xSQL.Schema.SqlServer.Filter;
using xSQL.SchemaCompare.SqlServer;

namespace xSQL.Sdk.SchemaCompare.Examples
{
    public class EntityFilters
    {
        private static EntityFilterManager CreateEntityFilters()
        {
            EntityFilterManager filterManager;
            EntityFilter viewFilter;
            EntityFilter procedureFilter;
            EntityFilter functionFilter;
            EntityFilterGroup group;


            // create the filter manager; set the exclusion type to ExcludeAlways
            filterManager = new EntityFilterManager(EntityFilterExclusionTypeEnum.ExcludeAlways);

            /*
             * Create a view filter that contains two conditions:
             *  - 1st condition excludes views the name of which start with tmp
             *  - 2nd condition excludes views the name of which end with temp
             * 
             * Conditions are applied to the view name, therefore they are created as EntityNameFilterCondition objects. 
             * 
             */
            viewFilter = new EntityFilter(SqlEntityTypeEnum.View);
            
            // create a new group
            group = new EntityFilterGroup(viewFilter);                        
            // combine the conditions in the group with the OR operator
            group.ConditionOperator = EntityFilterOperatorEnum.OR;
            // the group should EXCLUDE the views that match the conditions
            group.IncludeMatches = false;
            
            // add the group conditions
            group.Conditions.Add(new EntityNameFilterCondition("temp", EntityFilterConditionTypeEnum.StartingWith, group));
            group.Conditions.Add(new EntityNameFilterCondition("tmp", EntityFilterConditionTypeEnum.EndingWith, group));
            viewFilter.Groups.Add(group);
            
            // register the filter
            filterManager.Filters.Add(viewFilter);


            // create a procedure filter
            procedureFilter = new EntityFilter(SqlEntityTypeEnum.StoredProcedure);
            group = new EntityFilterGroup(procedureFilter);
            group.IncludeMatches = false;
            // add the condition that excludes procedures the name of which starts with tmp.
            group.Conditions.Add(new EntityNameFilterCondition("tmp", EntityFilterConditionTypeEnum.StartingWith, group));
            procedureFilter.Groups.Add(group);
            filterManager.Filters.Add(procedureFilter);


            /*
             * Create a function filter that contains two conditions:
             * - 1st condition includes the user-defined functions that belong to the schema Sales
             * - 2nd condition includes the user-defined functions that belong to the schema HumanResources. 
             * 
             * Conditions in this case are applied to the schema, so they are created as EntitySchemaFilterCondition objects. 
             * Since a function can satisfy either one of the conditions, the group filter combines them with the OR operator.
             * 
             * It is important to remember that the filter will exclude all other database functions that do not meet the filter's criteria. The only functions that will be compared and synchronized 
             * are the ones that belong to the schema Sales or HumanResources.
             * 
             */
            functionFilter = new EntityFilter(SqlEntityTypeEnum.UserDefinedFunction);
            
            // create a new group
            group = new EntityFilterGroup(functionFilter);
            // combine the conditions in the group with the OR operator
            group.ConditionOperator = EntityFilterOperatorEnum.OR;
            
            // add the group conditions; both conditions require an exact match of the schema, so the EqualsTo condition type is used
            group.Conditions.Add(new EntitySchemaFilterCondition("Sales", EntityFilterConditionTypeEnum.EqualsTo, group));
            group.Conditions.Add(new EntitySchemaFilterCondition("HumanResources", EntityFilterConditionTypeEnum.EqualsTo, group));
            functionFilter.Groups.Add(group);

            // register the filter
            filterManager.Filters.Add(functionFilter);

            
            return filterManager;
        }

        /// <summary>
        /// This example demonstrates a database comparison that uses entity filters. The synchronization script is generated but not executed.
        /// </summary>
        public static void CompareWithEntityFilters()
        {
            SqlServer xServer, yServer;
            SqlDatabase xDatabase, yDatabase;
            SqlSchemaCompare comparer;
            ScriptManager sqlScript;

            try
            {

                // create the SQL Server objects 
                xServer = new SqlServer(@"(local)");
                yServer = new SqlServer(@"(local)");

                // create the left database
                xDatabase = xServer.GetDatabase("Source");

                // create the right database
                yDatabase = yServer.GetDatabase("Target");

                // create the schema comparer
                comparer = new SqlSchemaCompare(xDatabase, yDatabase);

                // create the entity filters
                comparer.EntityFilterManager = CreateEntityFilters();

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
