using Zenject;

public class LandingInstaller : MonoInstaller
{
    public override void InstallBindings() => 
        Container.Bind<Landing>().AsSingle();
}
