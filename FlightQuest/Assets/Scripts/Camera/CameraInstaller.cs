using CameraOption;
using Zenject;

public sealed class CameraInstaller : MonoInstaller
{
    public override void InstallBindings() => Container.Bind<CameraManager>().FromComponentInHierarchy().AsSingle();
}
