using System;
using System.Linq;
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

        private Dictionary<string, string> keyedSerializedParameters = new Dictionary<string, string>();

        public ServiceRequest(string url, string parameters, string body)
        {
            Url = url;
            Parameters = parameters;
            Body = body;

            CreateKeyedSerializedParameters(parameters);
        }

        public T GetParameterValue<T>(string parameterKey)
        {
            return (T)Convert.ChangeType(keyedSerializedParameters[parameterKey], typeof(T));
        }

        private void CreateKeyedSerializedParameters(string parameters)
        {
            parameters.Split('&')
            .ToList()
            .Select(parameterPair => parameterPair.Split('='))
            .ToList()
            .ForEach(parameterKvp => keyedSerializedParameters.Add(parameterKvp[0], parameterKvp[1]));
        }
    }
}