using Amazon.ElasticTranscoder;
using Amazon.ElasticTranscoder.Model;
using Amazon.Runtime.Internal;
using log4net;
using Strimm.Model;
using Strimm.Model.Aws;
using StrimmBL.Exeptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StrimmBL.Processor
{
    public class ElasticTranscoderProcessor
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ElasticTranscoderProcessor));

        private static string AWS_ACCESS_KEY;
        private static string AWS_SECRET_KEY;
        private static string JOB_PRESET_720P;

        static ElasticTranscoderProcessor()
        {
            try
            {
                AWS_ACCESS_KEY = ConfigurationManager.AppSettings["AmazonAccessKey"];
                AWS_SECRET_KEY = ConfigurationManager.AppSettings["AmazonSecretKey"];
                JOB_PRESET_720P = ConfigurationManager.AppSettings["AwsElasticTranscoderJobPresetId"];
            }
            catch (Exception ex)
            {
                Logger.Error("Error occured while retrieving transcoder configuration", ex);
            }
        }

        public List<Pipeline> GetAllPipelines()
        {
            Logger.Info("Retrieving all pipelines from Elastic Transcoder with a callback");

            List<Pipeline> pipelines = new List<Pipeline>();

            try
            {
                using (var transcoder = new AmazonElasticTranscoderClient(AWS_ACCESS_KEY, AWS_SECRET_KEY, Amazon.RegionEndpoint.USEast1))
                {
                    ListPipelinesResponse response = transcoder.ListPipelines(new ListPipelinesRequest());

                    if (response != null)
                    {
                        pipelines = response.Pipelines ?? new List<Pipeline>();
                    }
                    else
                    {
                        throw new ElasticTranscoderException("Error occured while retrieving all transcoder pipelines");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ElasticTranscoderException("Error occured while retrieving all transcoder pipelines", ex);
            }

            return pipelines;
        }

        public void GetAllPipelinesAsync(Action<List<Pipeline>> callback)
        {
            Logger.Info("Retrieving all pipelines from Elastic Transcoder with a callback");

            try
            {
                using (var transcoder = new AmazonElasticTranscoderClient(AWS_ACCESS_KEY, AWS_SECRET_KEY, Amazon.RegionEndpoint.USEast1)) 
                {
                    transcoder.ListPipelinesAsync(new ListPipelinesRequest())
                          .ContinueWith((a) =>
                          {
                              if (a.IsCompleted)
                              {
                                  var pipelines = a.Result != null ? a.Result.Pipelines : new List<Pipeline>();
                                  callback(pipelines);
                              }
                              else if (a.IsFaulted)
                              {
                                  throw new ElasticTranscoderException("Error occured while retrieving all transcoder pipelines", a.Exception);
                              }
                          });
                }
            }
            catch (Exception ex)
            {
                throw new ElasticTranscoderException("Error occured while retrieving all transcoder pipelines", ex);
            }
        }

        public List<Job> GetAllPipelineJobsByPipelineId(string pipelineId)
        {
            Logger.Info(String.Format("Retrieving all pipeline jobs from Elastic Transcoder for pipeline with id={0}", pipelineId));

            List<Job> jobs = new List<Job>();

            try
            {
                using (var transcoder = new AmazonElasticTranscoderClient(AWS_ACCESS_KEY, AWS_SECRET_KEY, Amazon.RegionEndpoint.USEast1))
                {
                    ListJobsByPipelineResponse response = transcoder.ListJobsByPipeline(new ListJobsByPipelineRequest() { PipelineId = pipelineId });

                    if (response != null)
                    {
                        jobs = response.Jobs ?? jobs;
                    }
                    else
                    {
                        throw new ElasticTranscoderException(String.Format("Error occured while retrieving transcoder jobs for pipeline with id={0}", pipelineId));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ElasticTranscoderException(String.Format("Error occured while retrieving transcoder jobs for pipeline with id={0}", pipelineId), ex);
            }

            return jobs;
        }

        public void GetAllPipelineJobsByPipelineIdAsync(string pipelineId, Action<List<Job>> callback)
        {
            Logger.Info(String.Format("Retrieving all pipeline jobs from Elastic Transcoder for pipeline with id={0}", pipelineId));

            var jobs = new List<Job>();

            try
            {
                using (var transcoder = new AmazonElasticTranscoderClient(AWS_ACCESS_KEY, AWS_SECRET_KEY, Amazon.RegionEndpoint.USEast1))
                {
                    transcoder.ListJobsByPipelineAsync(new ListJobsByPipelineRequest() { PipelineId = pipelineId })
                          .ContinueWith((a) =>
                          {
                              if (a.IsCompleted)
                              {
                                  jobs = a.Result != null ? a.Result.Jobs : jobs;
                                  callback(jobs);
                              }
                              else if (a.IsFaulted)
                              {
                                  throw new ElasticTranscoderException(String.Format("Error occured while retrieving transcoder jobs for pipeline with id={0}", pipelineId), a.Exception);
                              }
                          });
                }
            }
            catch (Exception ex)
            {
                throw new ElasticTranscoderException(String.Format("Error occured while retrieving transcoder jobs for pipeline with id={0}", pipelineId), ex);
            }
        }

        public void CancelJobByIdAsync(string jobId, Action<bool> callback)
        {
            Logger.Info(String.Format("Attempting to cancel an existing job with id={0}", jobId));

            try
            {
                using (var transcoder = new AmazonElasticTranscoderClient(AWS_ACCESS_KEY, AWS_SECRET_KEY, Amazon.RegionEndpoint.USEast1))
                {
                    transcoder.ReadJobAsync(new ReadJobRequest() { Id = jobId })
                          .ContinueWith((jr) =>
                          {
                              if (jr.Result != null && jr.Result.Job != null)
                              {
                                  var job = jr.Result.Job;
                                  if (job.Status == "Progressing")
                                  {
                                      transcoder.CancelJobAsync(new CancelJobRequest() { Id = jobId })
                                                .ContinueWith((a) =>
                                                {
                                                    if (a.IsCompleted)
                                                    {
                                                        callback(true);
                                                    }
                                                    else if (a.IsFaulted)
                                                    {
                                                        throw new ElasticTranscoderException(String.Format("Error occured while canceling job with id={0}", jobId), a.Exception);
                                                    }
                                                });
                                  }
                                  else
                                  {
                                      Logger.Warn(String.Format("Unable to cancel job id={0}. Job has status of {1} and is no longer being processed", jobId, job.Status));
                                  }
                              }
                              else
                              {
                                  Logger.Warn(String.Format("Invalid job id={0} specified. Job cannot be canceled", jobId));
                              }
                          });
                }
            }
            catch (WebException web)
            {
                throw new ElasticTranscoderException(String.Format("Error occured while canceling job with id={0}", jobId), web);
            }
            catch (HttpErrorResponseException web)
            {
                throw new ElasticTranscoderException(String.Format("Error occured while canceling job with id={0}", jobId), web);
            }
            catch (Exception ex)
            {
                throw new ElasticTranscoderException(String.Format("Error occured while canceling job with id={0}", jobId), ex);
            }
        }

        public Job CreateJob(string pipelineId, string inputVideoFileKey, string outputVideoFileKey)
        {
            Logger.Info(String.Format("Creating a new Job to transcode video file '{0}' into '{1}' using pipeline with id={2}", inputVideoFileKey, outputVideoFileKey, pipelineId));

            Job job = null;

            try
            {
                using (var transcoder = new AmazonElasticTranscoderClient(AWS_ACCESS_KEY, AWS_SECRET_KEY, Amazon.RegionEndpoint.USEast1))
                {
                    var jobRequest = new CreateJobRequest()
                    {
                        PipelineId = pipelineId,
                        Input = new JobInput()
                        {
                            AspectRatio = "auto",
                            Container = "mp4",
                            FrameRate = "auto",
                            Interlaced = "auto",
                            Resolution = "auto",
                            Key = inputVideoFileKey
                        },
                        Output = new CreateJobOutput()
                        {
                            PresetId = JOB_PRESET_720P,
                            Rotate = "0",
                            Key = outputVideoFileKey
                        }
                    };

                    CreateJobResponse response = transcoder.CreateJob(jobRequest);

                    if (response != null)
                    {
                        job = response.Job;
                    }
                    else
                    {
                        throw new ElasticTranscoderException(String.Format("Failed to create new Elastic Transcoder job in pipeline with id={0} for video file={1}", pipelineId, inputVideoFileKey));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ElasticTranscoderException(String.Format("Failed to create new Elastic Transcoder job in pipeline with id={0} for video file={1}", pipelineId, inputVideoFileKey), ex);
            }

            return job;
        }

        public void CreateJobAsync(string pipelineId, string inputVideoFileKey, string outputVideoFileKey, Action<Job> callback)
        {
            Logger.Info(String.Format("Creating a new Job to transcode video file '{0}' into '{1}' using pipeline with id={2}", inputVideoFileKey, outputVideoFileKey, pipelineId));
            
            try
            {
                using (var transcoder = new AmazonElasticTranscoderClient(AWS_ACCESS_KEY, AWS_SECRET_KEY, Amazon.RegionEndpoint.USEast1))
                {
                    var jobRequest = new CreateJobRequest()
                    {
                        PipelineId = pipelineId,
                        Input = new JobInput()
                        {
                            AspectRatio = "auto",
                            Container = "mp4",
                            FrameRate = "auto",
                            Interlaced = "auto",
                            Resolution = "auto",
                            Key = inputVideoFileKey
                        },
                        Output = new CreateJobOutput()
                        {
                            PresetId = JOB_PRESET_720P,
                            Rotate = "0",
                            Key = outputVideoFileKey
                        }
                    };

                    transcoder.CreateJobAsync(jobRequest)
                              .ContinueWith((t) =>
                              {
                                  if (!t.IsFaulted)
                                  {
                                      if (t.Result != null && t.Result.Job != null)
                                      {
                                          callback(t.Result.Job);
                                      }
                                  }
                                  else
                                  {
                                      throw new ElasticTranscoderException(String.Format("Failed to create new Elastic Transcoder job in pipeline with id={0} for video file={1}", pipelineId, inputVideoFileKey), t.Exception);
                                  }
                              });
                }
            }
            catch (Exception ex) 
            {
                throw new ElasticTranscoderException(String.Format("Failed to create new Elastic Transcoder job in pipeline with id={0} for video file={1}", pipelineId, inputVideoFileKey), ex);
            }
        }
    }
}
