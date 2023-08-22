using UnityEngine;
using System;

public class PrefabInitializationNotifier : MonoBehaviour
{
    public event Action OnPrefabInitialized;

    public void NotifyPrefabInitialized() => OnPrefabInitialized?.Invoke();
}
