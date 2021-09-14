using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Strimm.WebApi.HttpResponse
{
    public class DataOperationResponse
    {
        private DataOperationStatus status;
        private OperationType type;
        private HttpRequestMessage request;
        private Exception exception;
        private object data;
        private string message;

        public DataOperationResponse(HttpRequestMessage request)
        {
            this.Request = request;
        }
        public DataOperationStatus Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }

        public OperationType Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }

        public HttpRequestMessage Request
        {
            get
            {
                return this.request;
            }
            private set
            {
                this.request = value;
            }
        }

        public Exception Exception
        {
            get
            {
                return this.exception;
            }
            set
            {
                this.exception = value;
            }
        }

        public object Data
        {
            get
            {
                return this.data;
            }
            set
            {
                this.data = value;
            }
        }

        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                this.message = value;
            }
        }
    }
}