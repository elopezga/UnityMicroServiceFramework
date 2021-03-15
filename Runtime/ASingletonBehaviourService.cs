using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RSG;

namespace ByteSize.MSF
{
    public abstract class ASingletonBehaviourService<T> : ABehaviourService
    {
        public static IPromise<T> Instance()
        {
            return instance == null ? CreateInstance() : Promise<T>.Resolved(instance);
        }

        protected new void Awake()
        {
            base.Awake();

            DontDestroyOnLoad(this.gameObject);
            instantiatedPromise.Resolve(instance);
        }

        private static T instance;
        private static Promise<T> instantiatedPromise = new Promise<T>();

        private static IPromise<T> CreateInstance()
        {
            GameObject singletonServiceGameObject = new GameObject(typeof(T).Name, typeof(T));
            instance = singletonServiceGameObject.GetComponent<T>();

            return instantiatedPromise;
        }
    }
}