using Zenject;

public sealed class FinishInstaller : MonoInstaller
{
    public override void InstallBindings() => Container.Bind<FinishLine>().AsSingle();
}