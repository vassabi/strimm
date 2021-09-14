using Dapper;
using log4net;
using Strimm.Data.Interfaces;
using Strimm.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Data.Repositories
{
    public class ElasticTranscoderJobRepository : RepositoryBase, IElasticTranscoderJobRepository
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ElasticTranscoderJobRepository));

        public ElasticTranscoderJob Insert(int userId, string pipelineId, string jobId, string jobArn, string inputKey, 
            string outputKey, string jobStatus, string presetId, int videoTubeId)
        {
            Contract.Requires(userId <= 0, "Specified user id is invalid");
            Contract.Requires(!String.IsNullOrEmpty(pipelineId), "Specified pipeline id is missing or invalid");
            Contract.Requires(!String.IsNullOrEmpty(jobId), "Specified job id is missing or invalid");
            Contract.Requires(!String.IsNullOrEmpty(jobArn), "Specified job ARN is missing or invalid");
            Contract.Requires(!String.IsNullOrEmpty(inputKey), "Specified job input key is missing or invalid");
            Contract.Requires(!String.IsNullOrEmpty(outputKey), "Specified job output key is missing or invalid");
            Contract.Requires(!String.IsNullOrEmpty(jobStatus), "Specified job status is missing or invalid");
            Contract.Requires(!String.IsNullOrEmpty(presetId), "Specified preset id is missing or invalid");
            Contract.Requires(videoTubeId <= 0, "Specified video tube id is invalid");

            ElasticTranscoderJob transcoderJob = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<ElasticTranscoderJob>("strimm.InsertElasticTranscoderJobWithGet", new
                    {
                        UserId = userId,
                        PipelineId = pipelineId,
                        JobId = jobId,
                        JobArn = jobArn,
                        InputKey = inputKey,
                        OutputKey = outputKey,
                        PresetId = presetId,
                        Status = jobStatus,
                        VideoTubeId = videoTubeId
                    }, null, false, 30, commandType: CommandType.StoredProcedure);
                    transcoderJob = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to insert new AWS Elastic Transcoding Job with Id={0} for User Id={1} and Video Tube Id='{2}", jobId, userId, videoTubeId), ex);
            }

            return transcoderJob;
        }

        public ElasticTranscoderJob Update(ElasticTranscoderJob existingJob)
        {
            Contract.Requires(existingJob == null, "Specified Elastic Transcoder Job is missing or invalid");
            Contract.Requires(existingJob != null && String.IsNullOrEmpty(existingJob.JobId), "Specified Elastic Transcoder Job Id is invalid");

            ElasticTranscoderJob transcoderJob = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<ElasticTranscoderJob>("strimm.UpdateElasticTranscoderJobStatusById", new
                    {
                        AwsJobId = existingJob.JobId,
                        JobStatus = existingJob.Status
                    }, null, false, 30, commandType: CommandType.StoredProcedure);
                    transcoderJob = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to update status of Elastic Transcoding Job with Id={0} to '{1}'", existingJob.JobId, existingJob.Status), ex);
            }

            return transcoderJob;
        }

        public ElasticTranscoderJob GetJobByExternalId(string awsJobId)
        {
            Contract.Requires(!String.IsNullOrEmpty(awsJobId), "Specified AWS Elastic Transcoder Job Id is missing or invalid");

            ElasticTranscoderJob transcoderJob = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<ElasticTranscoderJob>("strimm.GetElasticTranscoderJobByAwsJobId", new
                    {
                        AwsJobId = awsJobId
                    }, null, false, 30, commandType: CommandType.StoredProcedure);
                    transcoderJob = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve AWS Elastic Transcoding Job with AWS Id={0}", awsJobId), ex);
            }

            return transcoderJob;
        }

        public ElasticTranscoderJob GetJobById(int jobId)
        {
            Contract.Requires(jobId <= 0, "Specified Elastic Transcoder Job Id is missing or invalid");

            ElasticTranscoderJob transcoderJob = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<ElasticTranscoderJob>("strimm.GetElasticTranscoderJobById", new
                    {
                        ElasticTranscoderJobId = jobId
                    }, null, false, 30, commandType: CommandType.StoredProcedure);
                    transcoderJob = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve Elastic Transcoding Job with Id={0}", jobId), ex);
            }

            return transcoderJob;
        }

        public IList<ElasticTranscoderJob> GetAllJobByStatus(string status)
        {
            Contract.Requires(!String.IsNullOrEmpty(status), "Specified Elastic Transcoder Job status is missing or invalid");

            IList<ElasticTranscoderJob> transcoderJobs = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    transcoderJobs = this.StrimmDbConnection.Query<ElasticTranscoderJob>("strimm.GetElasticTranscoderJobsByStatus", new
                    {
                        JobStatus = status
                    }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve Elastic Transcoding Jobs with status of {0}", status), ex);
            }

            return transcoderJobs;
        }

        public IList<ElasticTranscoderJob> GetAllJobsByPipelineId(string awsPipelineId)
        {
            Contract.Requires(!String.IsNullOrEmpty(awsPipelineId), "Specified AWS Elastic Transcoder pipeline name is missing or invalid");

            IList<ElasticTranscoderJob> transcoderJobs = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    transcoderJobs = this.StrimmDbConnection.Query<ElasticTranscoderJob>("strimm.GetElasticTranscoderJobsByPipelineId", new
                    {
                        PipelineId = awsPipelineId
                    }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve Elastic Transcoding Jobs for pipeline id='{0}'", awsPipelineId), ex);
            }

            return transcoderJobs;
        }

        public bool AddVideoToVideoTranscodingQueue(int userId, int videoTubeId)
        {
            Contract.Requires(userId <= 0, "Specified user id is invalid");
            Contract.Requires(videoTubeId <= 0, "Specified video tube id is invalid");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rows = this.StrimmDbConnection.Execute("strimm.InsertVideoTranscodingJobQueueItem", new
                    {
                        UserId = userId,
                        VideoTubeId = videoTubeId
                    }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rows == 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to add video transcoding job to a queue for video tube id={0} based on user request with id={1}", videoTubeId, userId), ex);
            }

            return isSuccess;

        }

        public bool InsertElasticTranscoderJobNotification(int elasticTranscoderJobId, string messageId, string content, string jobStatus, DateTime receivedDateTime)
        {
            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rows = this.StrimmDbConnection.Execute("strimm.InsertElasticTranscoderJobNotification", new
                    {
                        ElasticTranscoderJobId = elasticTranscoderJobId,
                        MessageId = messageId,
                        Content = content,
                        JobStatus = jobStatus,
                        ReceivedDateTime = receivedDateTime
                    }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rows == 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to add video transcoding job notification for job id={0}, message id={1}, received on '{2}' with job status '{3}'",
                    elasticTranscoderJobId, messageId, receivedDateTime, jobStatus), ex);
            }

            return isSuccess;
        }
    }
}
