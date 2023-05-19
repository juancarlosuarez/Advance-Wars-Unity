using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Extensibles;
using UnityEngine;
public static class ChangePositionCanvas
{
  

  public static async Task MoveElement(this Transform transform, Vector3 destination, CancellationTokenSource ctc)
  {
    while (transform.transform.position != destination)
    {
        if (ctc.IsCancellationRequested)
        {
            ctc.Dispose();
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, destination, .3f);
        await Task.Yield();
    }
  }
  public static async Task MoveScaleYTest(this Transform transform, GameObject objectAffected)
  {
    while (objectAffected.transform.localScale.y > 0)
    {
        var scaleObject = objectAffected.transform.localScale;
        objectAffected.transform.localScale = new Vector2(scaleObject.x, scaleObject.y - 0.01f);
        await Task.Yield();
    }
  }
}
