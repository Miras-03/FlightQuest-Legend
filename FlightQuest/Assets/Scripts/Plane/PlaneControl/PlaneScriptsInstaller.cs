using PlaneSection;
using UnityEngine;
using Zenject;

public class PlaneScriptsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SpeedManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlaneExplosion>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PropellerRotate>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PointDetector>().FromComponentInHierarchy().AsSingle();
        Container.Bind<LandingGear>().FromComponentInHierarchy().AsSingle();

        Container.Bind<ParticleSystem>().FromComponentInHierarchy().AsSingle();
    }
}
