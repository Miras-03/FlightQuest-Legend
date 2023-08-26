using UnityEngine;
using Zenject;

public sealed class ButtonInstaller : MonoInstaller
{
    public override void InstallBindings() => 
        Container.Bind<StoreButtonController>().FromComponentInHierarchy().AsSingle();
}
