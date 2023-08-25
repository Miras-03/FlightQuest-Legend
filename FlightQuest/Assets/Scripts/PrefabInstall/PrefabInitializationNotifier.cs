using UnityEngine;
using System;

public sealed class PrefabInitializationNotifier : MonoBehaviour
{
    public event Action OnPrefabInitialized;

    public void NotifyPrefabInitialized() => OnPrefabInitialized?.Invoke();
}
