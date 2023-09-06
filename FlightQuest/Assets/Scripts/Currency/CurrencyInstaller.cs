using Zenject;

public sealed class CurrencyInstaller : MonoInstaller
{ 
    public override void InstallBindings()
    {
        Container.Bind<Currency>().AsSingle();
        Container.Bind<CurrencyManager>().FromComponentInHierarchy().AsSingle();
    }
}