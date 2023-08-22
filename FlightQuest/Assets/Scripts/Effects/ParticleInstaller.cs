using UnityEngine;
using Zenject;

public class ParticleInstaller : MonoInstaller
{
    public override void InstallBindings() => 
        Container.Bind<ParticleSystemManager>().FromComponentInHierarchy().AsSingle();
}
