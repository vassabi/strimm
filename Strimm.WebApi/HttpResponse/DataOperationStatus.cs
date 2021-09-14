using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Strimm.WebApi.HttpResponse
{
    public enum DataOperationStatus
    {
        Success,
        NotFound,
        Exception,
        Forbidden
    }
}