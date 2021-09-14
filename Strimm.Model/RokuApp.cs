using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model
{
    public class RokuApp
    {
        public int UserID { get; set; }
        public string AppName { get; set; }
        public string AdLink { get; set; }
        public string About { get; set; }
        public string PrivacyPolicyLink { get; set; }
        public byte[] ImageHD { get; set; }
        public byte[] ImageSD { get; set; }
        public Guid ApiKey { get; set; }
    }
}
