using UnityEngine;
using Zenject;

public class ExecuteFinishInstaller : MonoInstaller
{
    public override void InstallBindings() => 
        Container.Bind<ExecuteFinishObservers>().FromComponentInHierarchy().AsSingle();
}