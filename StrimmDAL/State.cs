using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace StrimmDAL
{
   public class State
    {
       [Key]
       public int stateId { get; set; }
       public string state { get; set; }
       [ForeignKey("countryId")]
       public Country country { get; set; }
       public int countryId { get; set; }
    }
}
