using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Strimm.AutomationWinService.Models
{
    public class StrimmJobModel
    {
        private string groupName;
        private string jobKey;
        private string description;
        private string triggerKey;
        private string triggerGroup;
        private string triggerState;
        private string nextRunTime;
        private string lastRunTime;
        private string triggerType;

        public string GroupName
        {
            get
            {
                return this.groupName;
            }
            set
            {
                this.groupName = value;
            }
        }

        public string JobKey
        {
            get
            {
                return this.jobKey;
            }
            set
            {
                this.jobKey = value;
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }

        public string TriggerKey
        {
            get
            {
                return this.triggerKey;
            }
            set
            {
                this.triggerKey = value;
            }
        }

        public string TriggerGroup
        {
            get
            {
                return this.triggerGroup;
            }
            set
            {
                this.triggerGroup = value;
            }
        }

        public string TriggerState
        {
            get
            {
                return this.triggerState;
            }
            set
            {
                this.triggerState = value;
            }
        }

        public string LastRunTime
        {
            get
            {
                return this.lastRunTime;
            }
            set
            {
                this.lastRunTime = value;
            }
        }

        public string NextRunTime
        {
            get
            {
                return this.nextRunTime;
            }
            set
            {
                this.nextRunTime = value;
            }
        }

        public string TriggerType
        {
            get
            {
                return this.triggerType;
            }
            set
            {
                this.triggerType = value;
            }
        }
    }
}
