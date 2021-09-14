using System;
using System.Runtime.Serialization;

namespace Strimm.Model
{
    [DataContract]
    public class VideoRoomTube : ModelBase
    {
        private int videoRoomTubeId;
        private int userId;
        private bool isPrivate;

        [DataMember]
        public int VideoRoomTubeId
        {
            get
            {
                return this.videoRoomTubeId;
            }
            set
            {
                this.videoRoomTubeId = value;
            }
        }

        [DataMember]
        public int UserId
        {
            get
            {
                return this.userId;
            }
            set
            {
                this.userId = value;
            }
        }

        [DataMember]
        public bool IsPrivate
        {
            get
            {
                return this.isPrivate;
            }
            set
            {
                this.isPrivate = value;
            }
        }
    }
}
