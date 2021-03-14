using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RSG;

namespace ByteSize.MSF
{
    public interface IService
    {
        string ServiceName { get; }

        IPromise RespondToRequest(ServiceRequest serviceRequest);
    }
}