using Zenject;

public class FinishInstaller : MonoInstaller
{
    public override void InstallBindings() => Container.Bind<FinishLine>().AsSingle();
}