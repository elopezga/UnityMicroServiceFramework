using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ByteSize.MSF
{
    public class ServiceRequest
    {
        public readonly string Url;
        public readonly string Parameters;
        public readonly string Body;

        public ServiceRequest(string url, string parameters, string body)
        {
            Url = url;
            Parameters = parameters;
            Body = body;
        }
    }
}