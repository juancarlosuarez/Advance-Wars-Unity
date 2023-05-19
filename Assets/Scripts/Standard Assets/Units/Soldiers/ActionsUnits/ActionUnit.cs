using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class ActionUnit : MonoBehaviour
{
    public virtual void Trigger() { }
    public virtual void SharedVoid() { }
    public virtual void Init() { }

    public bool thisActionMaybeRequiresMovement;
    private void Start()
    {
        Init();
    }

}

