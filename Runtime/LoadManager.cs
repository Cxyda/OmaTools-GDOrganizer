using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Data.Events;
using JetBrains.Annotations;
using Plugins.O.M.A.Games.Core;
using UnityEngine;
//using Zenject;

namespace Utility
{
    /// <summary>
    /// This is only a container class for Zenject to inject the possibility of starting coroutines here
    /// </summary>
    public class LoadManager : MonoBehaviour
    {
        public enum LoadingJobSize
        {
            Tiny = 1,
            Small = 2,
            Medium = 4,
            Big = 8,
            Huge = 16
        }

        public float LoadingProgress
        {
            get { return ((float)_finishedLoad / _loadWeight) * 100; }
        }
        private int _loadWeight;
        private int _finishedLoad;

        private IMessageBroker _messageHub;

        private readonly Dictionary<int, LoadingJobSize> _loadingOperations =
            new Dictionary<int, LoadingJobSize>();

 //       [Inject]
        public void Construct(IMessageBroker messageHub)
        {
            _messageHub = messageHub;
        }

        public int StartLoadingProcess(LoadingJobSize jobSize, [CallerFilePath] [NotNull] string caller = "foo")
        {
            _loadWeight += (int)jobSize;
            var jobId = caller.GetHashCode();
            _loadingOperations[jobId] = jobSize;
            return jobId;
        }

        public void LoadingProcessFinished(int jobId)
        {
            var jobSize = _loadingOperations[jobId];

            _loadingOperations.Remove(jobId);
            _finishedLoad += (int)jobSize;

            if (_loadingOperations.Count != 0) return;

            Debug.Log(":: -- All Loading Jobs done! Starting the game ....");
            _messageHub.Publish(new GameLoadedEvent());
        }
    }
}