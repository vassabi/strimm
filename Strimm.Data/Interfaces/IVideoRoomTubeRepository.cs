using Strimm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Data.Interfaces
{
    public interface IVideoRoomTubeRepository
    {
        VideoRoomTube GetVideoRoomTubeById(int videoRoomTubeId);

        VideoRoomTube GetVideoRoomTubeByUserId(int userId);

        bool InsertVideoRoomTube(int userId, bool isPrivate = false);

        VideoRoomTube InsertVideoRoomTubeWithGet(int userId, bool isPrivate = false);
    }
}
