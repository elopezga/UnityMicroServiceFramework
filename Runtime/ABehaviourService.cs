using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RSG;

namespace ByteSize.MSF
{
    public abstract class ABehaviourService : MonoBehaviour, IService
    {
        public string ServiceName { get { return this.name; } }

        public abstract IPromise RespondToRequest(ServiceRequest serviceRequest);

        protected abstract string serviceUrl { get; }

        protected void Awake()
        {
            StartCoroutine(RegisterService());
        }

        private IEnumerator RegisterService()
        {
            while (ServiceRequestRouter.Instance == null)
            {
                yield return null;
            }

            ServiceRequestRouter.Instance.RegisterService(serviceUrl, this);
        }
    }
}