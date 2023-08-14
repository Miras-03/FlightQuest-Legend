using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    public override void InstallBindings() => Container.Bind<UIInstaller>().AsSingle();
}