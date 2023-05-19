using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
public static class AssetReferenceExtension
{
    public static Task<T> LoadAssetAsyncAsTask<T>(this AssetReference assetReference)
    {
        var taskCompletionSource = new TaskCompletionSource<T>();
        var asyncOperationHandle = assetReference.LoadAssetAsync<T>();
        asyncOperationHandle.Completed += handle =>
        {
            taskCompletionSource.SetResult(handle.Result);
        };

        return Task.Run(() => taskCompletionSource.Task);
    }
}
