using Strimm.Model.WebModel;
using StrimmBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace Strimm.Web.Services
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SearchWebService
    {
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";

        /// <summary>
        /// This method will find and retrieve all video channels by keywords
        /// </summary>
        /// <param name="keywords">Collection of keywords</param>
        /// <returns></returns>
        [OperationContract]
        public List<ChannelTubeModel> GetChannelsByKeywords(List<string> keywords)
        {
            return SearchManage.GetChannelsByKeywords(keywords);
        }

        /// <summary>
        /// This method will find and retrieve all users by keywords
        /// </summary>
        /// <param name="keywords">Collection of keywords</param>
        /// <returns></returns>
        [OperationContract]
        public List<UserModel> GetUsersByKeywords(List<string> keywords)
        {
            return SearchManage.GetUsersByKeywords(keywords);
        }
    }
}
