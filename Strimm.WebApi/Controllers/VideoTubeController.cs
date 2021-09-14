using Strimm.Data.Interfaces;
using Strimm.Model.WebModel;
using Strimm.WebApi.HttpResponse;
using Strimm.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Strimm.WebApi.Controllers
{
    public class VideoTubeController : BaseApiController
    {
        public IVideoTubeRepository videoTubeRepository;

        public VideoTubeController(IVideoTubeRepository repository)
        {
            this.videoTubeRepository = repository;
        }

        [HttpGet]
        [Route("api/public/videos/{pageIndex:int:min(0)}/{pageSize:int:min(1)}", Name="PublicVideoPage")]
        public async Task<HttpResponseMessage> GetVideoTubeFromPublicLibraryByPageIndex(int pageIndex, int pageSize = 10)
        {
            return await Task.Run<HttpResponseMessage>(() =>
            {
                var response = new DataOperationResponse(this.Request);
                VideoTubePageModel publicVideoPage = null;

                try
                {
                    pageIndex = pageIndex < 0 ? 0 : pageIndex;

                    int pageCount = 0;
                    var pageOfPublicVideos = videoTubeRepository.GetPublicVideoTubesByPageIndex(pageIndex, out pageCount, pageSize);

                    var modelFactory = new ModelFactory(this.Request);
                    
                    publicVideoPage = modelFactory.CreateVideoPageModel(pageIndex, pageCount, pageSize, pageOfPublicVideos);

                    if (publicVideoPage != null)
                    {
                        response.Status = DataOperationStatus.Success;
                        response.Type = OperationType.Get;
                        response.Data = publicVideoPage;
                    }
                    else
                    {
                        response.Status = DataOperationStatus.NotFound;
                        response.Type = OperationType.Get;
                        response.Message = "System error. Unable to find public videos";
                    }
                }
                catch (Exception ex)
                {
                    response.Status = DataOperationStatus.Exception;
                    response.Exception = ex;
                    response.Type = OperationType.Get;
                }

                return SetupHttpResponse(response);
            });
        }

        [HttpGet]
        [Route("api/videos/{videoId:int:min(1)}", Name="VideoTube")]
        public async Task<HttpResponseMessage> GetById(int videoId)
        {
            return await Task.Run<HttpResponseMessage>(() =>
            {
                var response = new DataOperationResponse(this.Request);

                try
                {
                    if (videoId <= 0)
                    {
                        throw new Exception("Invalid video tube id specified");
                    }

                    var modelFactory = new ModelFactory(this.Request);
                    var videoTube = videoTubeRepository.GetVideoTubePoById(videoId);
                    var videoTubeModel = modelFactory.CreateVideoTubeModel(videoTube);

                    if (videoTubeModel != null)
                    {
                        response.Status = DataOperationStatus.Success;
                        response.Type = OperationType.Get;
                        response.Data = videoTubeModel;
                    }
                    else
                    {
                        response.Status = DataOperationStatus.NotFound;
                        response.Type = OperationType.Get;
                        response.Message = String.Format("System error. Unable to find video with Id={0}", videoId);
                    }
                }
                catch (Exception ex)
                {
                    response.Status = DataOperationStatus.Exception;
                    response.Exception = ex;
                    response.Type = OperationType.Get;
                }

                return SetupHttpResponse(response);
            });
        }
    }
}
