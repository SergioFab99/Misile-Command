using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public abstract class BaseEntity : MonoBehaviour
{
    public abstract void Initialize();
    public abstract void OnDestroyed();

    protected virtual void OnEnable() => Initialize();
    protected virtual void OnDisable() => OnDestroyed();
}
