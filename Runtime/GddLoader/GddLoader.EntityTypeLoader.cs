using System.Collections;
using System.Collections.Generic;
using Data.EntityDefinition;
using Logic;
using UnityEngine;
using Utility;

namespace Services.GddService
{
    /// <summary>
    /// TODO:
    /// </summary>
    public static partial class GddLoader
    {
        private struct EntityTypeLoader <T> where T : ScriptableObject, IEntityDefinition
        {
            private LoadManager _runner;
            private int _processId;

            public Dictionary<EntityType, T> DataMap;

            public void LoadAll(LoadManager runner)
            {
                IList<object> labelsToLoad = new List<object>() {"EntityTypes"};

                _runner = runner;
                DataMap = new Dictionary<EntityType, T>();
                _processId = _runner.StartLoadingProcess(LoadManager.LoadingJobSize.Small);
                //Addressables.LoadAssetsAsync<T>(labelsToLoad, LoadingEntityComplete, Addressables.MergeMode.UseFirst)
                //    .Completed += LoadingAllEntityDefinitionsComplete;
            }
/*
            private void LoadingAllEntityDefinitionsComplete(AsyncOperationHandle<IList<T>> asyncOperationHandle)
            {
                _runner.LoadingProcessFinished(_processId);
            }
*/
            private void LoadingEntityComplete(T obj)
            {
                DataMap.Add(obj.EntityType, obj);
            }

            private IEnumerator LoadFromMod(string modname)
            {
                //TODO
                yield return null;
            }
        }
    }
}