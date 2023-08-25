using Zenject;

public class CurrencyInstaller : MonoInstaller
{
    public override void InstallBindings() => Container.Bind<Currency>().AsSingle();
}