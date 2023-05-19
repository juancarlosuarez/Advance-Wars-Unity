using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtensible
{
    public static void SetParent(this GameObject children, GameObject parent) => children.transform.SetParent(parent.transform);
}
