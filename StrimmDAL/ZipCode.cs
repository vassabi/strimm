using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace StrimmDAL
{
   public class ZipCode
    {
        [Key]
        public int ZipID { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int AC { get; set; }
        public string County { get; set; }
        public string TimeZone { get; set; }
        public string DST { get; set; }
        public string StateAbr { get; set; }
    }
}
