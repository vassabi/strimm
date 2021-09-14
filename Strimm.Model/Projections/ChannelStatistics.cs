using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Projections
{
  public  class ChannelStatistics:ChannelTubePo
    {
      public double visitTime;
      public DateTime? lastVisit;
      public int visitorCount;
      public string channelOwnerEmail;
      public bool isOnAir;
      public bool isProEnabled;
      public string channelOwnerFirstName;
      public double VisitTime
      {
          get
          {
              return this.visitTime;
          }
          set
          {
              this.visitTime = value;
          }
      }

      public DateTime? LastVisit
      {
          get
          {
              return this.lastVisit;
          }
          set
          {
              this.lastVisit = value;
          }
      }

      public int VisitorCount
      {
          get
          {
              return this.visitorCount;
          }
          set
          {
              this.visitorCount = value;
          }
      }

      public string ChannelOwnerEmail
      {
          get
          {
              return this.channelOwnerEmail;
          }
          set
          {
              this.channelOwnerEmail = value;
          }
      }

      public bool IsOnAir
      {
          get
          {
              return this.isOnAir;
          }
          set
          {
              this.isOnAir = value;
          }
      }
      public bool IsProEnabled
      {
          get
          {
              return this.isProEnabled;
          }
          set
          {
              this.isProEnabled = value;
          }
      }

      public string ChannelOwnerFirstName
      {
          get
          {
              return this.channelOwnerFirstName;
          }
          set
          {
              this.channelOwnerFirstName = value;
          }
      }
    }
}
