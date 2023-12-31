using Zenject;

public class AdsInstaller : MonoInstaller
{
    public override void InstallBindings() =>
        Container.Bind<InterstitialAd>().FromComponentInHierarchy().AsSingle();
}