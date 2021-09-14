using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Projections
{
    public class EmbeddedChannelPo
    {
        private int channelTubeId;
        private string name;
        private bool isSingleChannelView;
        private bool isSubscribedDomain;
        private string embeddedHostUrl;
        private int loadCount;
        private int userCount;
        private string userName;
        private string accountNumber;
        private DateTime? loadDateEnd;
        private DateTime loadDate;
        private double visitTime;
        private DateTime dateOfEmbedding;

        public int ChannelTubeId
        {
            get { return this.channelTubeId; }
            set { this.channelTubeId = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public string Username
        {
            get { return this.userName; }
            set { this.userName = value; }
        }
         public string AccountNumber
        {
            get { return this.accountNumber; }
            set { this.accountNumber = value; }
        }
        public string EmbeddedHostUrl
        {
            get { return this.embeddedHostUrl; }
            set { this.embeddedHostUrl = value; }
        }
        public bool IsSingleChannelView
        {
            get { return this.isSingleChannelView; }
            set { this.isSingleChannelView = value; }
        }

        public bool IsSubscribedDomain
        {
            get { return this.isSubscribedDomain; }
            set { this.isSubscribedDomain = value; }
        }
        public int LoadCount
        {
            get { return this.loadCount; }
            set { this.loadCount = value; }
        }
        public int UserCount
        {
            get { return this.userCount; }
            set { this.userCount = value; }
        }

        public DateTime? LoadDateEnd
        {
            get { return this.loadDateEnd; }
            set { this.loadDateEnd = value; }
        }
        public DateTime LoadDate
        {
            get { return this.loadDate; }
            set { this.loadDate = value; }
        }

        public double VisitTime
        {
            get { return this.visitTime; }
            set { this.visitTime = value; }
        }

        public DateTime DateOfEmbedding
        {
            get { return this.dateOfEmbedding; }
            set { this.dateOfEmbedding = value; }
        }

        
    }
}
