using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class TaskExtension
{
    public static async void WrapError(this Task task)
    {
        await task;
    }
}
