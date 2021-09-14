using Strimm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Data.Interfaces
{
    interface IVideoProviderRepository
    {
        List<VideoProvider> GetAllVideoProviders();

        List<VideoProvider> GetActiveVideoProviders();
    }
}
