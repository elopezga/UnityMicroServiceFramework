# UnityMicroServiceFramework

A framework that allows you to create microservice-like objects and use them without explicitly referencing them.

## Getting Started
First, create your microservice:
```c#
    using ByteSize.MSF;
    using RSG; // You will need this C# Promise library: https://github.com/elopezga/C-Sharp-Promise

    public class SceneLoaderService : ASingletonBehaviourService<SceneLoaderService>
    {
        protected override string serviceUrl => "sceneloader.yourdomain";

        public override IPromise RespondToRequest(ServiceRequest serviceRequest)
        {   
            return LoadScene(); // Async operation that returns a Promise
        }
     }
```


Next, create a ServiceRequestRouter at the earliest stage of your app startup, in a bootload of some sort. Instantiate the SceneLoaderService singleton.
The SceneLoaderService will automatically register itself with the ServiceRequestRouter.
```c#
  using ByteSize.MSF;
  using RSG;
  public class Bootloader
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Awake()
        {
            CreateServiceRequestRouter()
            .Then(() => SceneLoaderService.Instance()); // Instantiate SceneLoaderService singleton
        }

        private static IPromise CreateServiceRequestRouter()
        {
            new GameObject(typeof(ServiceRequestRouter).Name, typeof(ServiceRequestRouter));

            return Promise.Resolved();
        }
    }
```

Later in your app, you can request from the SceneLoaderService:
```c#
  ServiceRequestRouter.Instance.SendRequest(new ServiceRequest("sceneloader.yourdomain", string.Empty, string.Empty));
```

The SceneLoaderService will then execute the RespondToRequest function and will resolve as soon as it is done.

## Passing Parameters
There are times where you will want to pass parameters to your microservice. You can pass parameters through url-like parameters:
```c#
    string requestParameters = $"scenetoload=mycoolscene&isAdditive=true";
    ServiceRequestRouter.Instance.SendRequest(new ServiceRequest("sceneloader.yourdomain", requestParameters, string.Empty));
```

You can then get these parameters in your microservice:
```c#
    public class SceneLoaderService : ASingletonBehaviourService<SceneLoaderService>
    {
        protected override string serviceUrl => "sceneloader.yourdomain";

        public override IPromise RespondToRequest(ServiceRequest serviceRequest)
        {
            string nameOfSceneToLoad = serviceRequest.GetParameterValue<string>("scenetoload");
            bool isAdditive = serviceRequest.GetParameterValue<bool>("isAdditive");

            return LoadScene(nameOfSceneToLoad, isAdditive);
        }
    }
```
