using Newtonsoft.Json;
using Strimm.WebApi.HttpResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace Strimm.WebApi.Controllers
{
    /**
     * 
     * */
    public class BaseApiController : ApiController
    {
        /**
         * 
         * */
        public static HttpContent GetHttpContent<T>(T data)
        {
            var jsonSerializer = new Newtonsoft.Json.JsonSerializer();
            HttpContent httpContent = data != null
                                            ? new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json")
                                            : null;

            return httpContent;
        }

        /**
         * 
         * */
        public static HttpContent GetHttpContent<T>(IEnumerable<T> data)
        {
            HttpContent httpContent = data != null
                                            ? new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json")
                                            : null;

            return httpContent;
        }

        /**
         * 
         * */
        public static HttpResponseMessage SetupHttpResponse(DataOperationResponse dataResponse, HttpContent content = null)
        {
            var response = new HttpResponseMessage();

            switch (dataResponse.Status)
            {
                case DataOperationStatus.Success:
                    {
                        if (dataResponse.Type == OperationType.Insert)
                        {
                            response.StatusCode = HttpStatusCode.Created;
                            response.Content = GetHttpContent(dataResponse.Data);
                            response.Content.Headers.Expires = new DateTimeOffset(DateTime.Now.AddSeconds(30));
                        }
                        else
                        {
                            response.StatusCode = HttpStatusCode.OK;
                            response.Content = content == null ? GetHttpContent(dataResponse.Data) : content;
                        }

                        break;
                    }
                case DataOperationStatus.NotFound:
                    {
                        // REST best practise on delete is to return OK if resource 
                        // cannot be found but here we are explicitly returning Not Found
                        response.StatusCode = HttpStatusCode.NotFound;
                        response.Content = new StringContent(dataResponse.Message);

                        break;
                    }
                case DataOperationStatus.Exception:
                    {
                        response.StatusCode = HttpStatusCode.InternalServerError;
                        response.Content = new StringContent(dataResponse.Exception.Message);

                        break;
                    }
                default:
                    break;

            }

            return response;
        }
    }
}