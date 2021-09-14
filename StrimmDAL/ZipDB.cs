using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace StrimmDAL
{
   public class ZipDB:DbContext
    {
       public ZipDB() : base("name=zip") { }
       public DbSet<ZipCode> ZipCode { get; set; }
    }
}
