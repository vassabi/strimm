using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace StrimmDAL
{
   public class TempUser
    {
        [Key]
        public int userId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string zip { get; set; }
        public DateTime birthDate { get; set; }
        public string accountNumber { get; set; }
        public DateTime registrationDate { get; set; }
        public DateTime expireDate { get; set; }
        public string gender { get; set; }
        public  string IP { get; set; }
        public bool isBlocked { get; set; }
        public string userName { get; set; }
    
       
    }
}
