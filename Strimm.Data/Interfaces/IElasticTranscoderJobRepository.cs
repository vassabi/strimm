using Strimm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Data.Interfaces
{
    public interface IElasticTranscoderJobRepository
    {
        ElasticTranscoderJob Insert(int userId, string pipelineId, string jobId, string jobArn, string inputKey,
            string outputKey, string jobStatus, string presetId, int videoTubeId);

        ElasticTranscoderJob Update(ElasticTranscoderJob existingJob);

        ElasticTranscoderJob GetJobByExternalId(string awsJobId);

        ElasticTranscoderJob GetJobById(int jobId);

        IList<ElasticTranscoderJob> GetAllJobByStatus(string status);

        IList<ElasticTranscoderJob> GetAllJobsByPipelineId(string awsPipelineId);
    }
}
