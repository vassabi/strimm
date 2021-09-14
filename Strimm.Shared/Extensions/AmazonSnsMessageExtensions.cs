using Amazon.SimpleNotificationService.Util;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Strimm.Shared.Extensions
{
    public static class AmazonSnsMessageExtensions
    {
        public static T DeserializeMessageDetails<T>(this Message message)
        {
            var jsonString = message.MessageText.Replace("\n", " ");

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(stream);
            return obj;
        }
    }
}
