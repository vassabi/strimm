using Strimm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Data.Interfaces
{
    public interface IEmailNotificationRepository
    {
        UnpublishedChannelScheduleEmail InsertUnpublishedChannelScheduleEmail(int channelScheduleId, string userEmail, DateTime timeSent);
    }
}
