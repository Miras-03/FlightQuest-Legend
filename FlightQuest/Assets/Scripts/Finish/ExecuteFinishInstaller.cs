using UnityEngine;
using Zenject;

public sealed class ExecuteFinishInstaller : MonoInstaller
{
    public override void InstallBindings() => 
        Container.Bind<ExecuteFinishObservers>().FromComponentInHierarchy().AsSingle();
}