using UnityEngine;
using Zenject;

public sealed class DeathInstaller : MonoInstaller
{
    public override void InstallBindings() => Container.Bind<PlayerDeath>().AsSingle();
}