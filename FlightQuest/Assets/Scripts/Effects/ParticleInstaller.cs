using UnityEngine;
using Zenject;

public sealed class ParticleInstaller : MonoInstaller
{
    public override void InstallBindings() => 
        Container.Bind<ParticleSystemManager>().FromComponentInHierarchy().AsSingle();
}
