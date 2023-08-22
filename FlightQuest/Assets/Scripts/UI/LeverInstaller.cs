using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class LeverInstaller : MonoInstaller
{
    public override void InstallBindings() => Container.Bind<Slider>().FromComponentInHierarchy().AsSingle();
}