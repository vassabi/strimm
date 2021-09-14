using Strimm.SqlCompare.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.SqlCompare.Model
{
    public class RunTimeParameters
    {
        public DbConnectionProperties SourceDbConnection { get; set; }
        public DbConnectionProperties TargetDbConnection { get; set; }
        public SqlAction ActionToPerform { get; set; }
        public List<KeyValuePair<DiffFilterType, string>> IncludeOptions { get; set; }
        public List<KeyValuePair<DiffFilterType, string>> ExcludeOptions { get; set; }
        public bool ScriptJiraItemsOnly { get; set; }
        public string CustomSnapshotFilePath { get; set; }
        public string CustomScriptFolderPath { get; set; }
        public string ExistingDiffFilePath { get; set; }
    }
}
