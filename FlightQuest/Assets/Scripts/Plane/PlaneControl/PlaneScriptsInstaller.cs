using PlaneSection;
using Zenject;

public sealed class PlaneScriptsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SpeedManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlaneExplosion>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PropellerRotate>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PointDetector>().FromComponentInHierarchy().AsSingle();
    }
}
