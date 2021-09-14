using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace StrimmDAL
{
   public class StrimmDB:DbContext
    {
       public StrimmDB() : base("name=strimm") { }
        public DbSet<User> User { get; set; }
        public DbSet<TempUser> TempUser { get; set; }
        public DbSet<Post> Post { get; set; }
       public DbSet<ChannelTube> ChannelTube { get; set; }
        public DbSet<ChannelCategory> ChannelCategory { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<CheckList> Checklist { get; set; }
        public DbSet<VideoRoomTube> vrTube { get; set; }
        public DbSet<VideoTube> videoTube { get; set; }
        public DbSet<ScheduleList> ScheduleList { get; set; }
        public DbSet<VideoSchedule> VideoSchedule { get; set; }
        public DbSet<ChannelSchedule> ChannelSchedule { get; set; }
        public DbSet<Archive> archive { get; set; }
        public DbSet<Follow> Follow { get; set; }
        public DbSet<ChannelSubscribe> ChannelSubscribe { get; set; }
        public DbSet<Rings> ring { get; set; }
        public DbSet<ErrorLog> errorLog { get; set; }
        public DbSet<ReservedNames> ReservedNames { get; set; }
        public DbSet<PublicLib> PublicLib { get; set; }
    
    }
}
