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
    public class PublicLibraryService
    {
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";

        /// <summary>
        /// This method will retrieve a page of public videos from public library based
        /// on specified page id
        /// </summary>
        /// <param name="pageIndex">Page Index</param>
        /// <param name="categoryId">Category id</param>
        /// <returns></returns>
        [OperationContract]
        public VideoTubePageModel GetAllPublicVideosByPageIndex(int pageIndex, int categoryId, int channelTubeId)
        {
            return categoryId > 0
                            ? PublicLibraryManage.GetAllPublicVideoTubesByCategoryIdAndPageIndex(categoryId, pageIndex, channelTubeId)
                            : PublicLibraryManage.GetAllPublicVideoTubesByPageIndex(pageIndex, channelTubeId);
        }
    }
}
