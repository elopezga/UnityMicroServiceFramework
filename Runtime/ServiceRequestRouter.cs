using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RSG;

namespace ByteSize.MSF
{
    public class ServiceRequestRouter : MonoBehaviour
    {
        public static ServiceRequestRouter Instance {  get { return instance; } }

        public IPromise SendRequest(ServiceRequest serviceRequest)
        {
            if (!IsServiceRegistered(serviceRequest.Url)) return Promise.Rejected(new System.Exception($"Service at url {serviceRequest.Url} is not registered."));

            return servicesMap[serviceRequest.Url].RespondToRequest(serviceRequest);
        }

        public void RegisterService(string serviceUrl, IService serviceInstance)
        {
            servicesMap.Add(serviceUrl, serviceInstance);

            Debug.Log($"Service {serviceInstance.ServiceName} registered successfully to url {serviceUrl}.");
        }

        private static ServiceRequestRouter instance;
        private Dictionary<string, IService> servicesMap = new Dictionary<string, IService>();

        private void Awake()
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        private bool IsServiceRegistered(string serviceUrl)
        {
            return servicesMap.ContainsKey(serviceUrl);
        }
    }
}