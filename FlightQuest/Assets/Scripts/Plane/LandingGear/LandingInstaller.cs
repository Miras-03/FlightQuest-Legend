using Zenject;

public sealed class LandingInstaller : MonoInstaller
{
    public override void InstallBindings() => Container.Bind<Landing>().AsSingle();
}
