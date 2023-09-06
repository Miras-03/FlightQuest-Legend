using Zenject;

public class PurchaseInfoInstaller : MonoInstaller
{
    public override void InstallBindings() => 
        Container.Bind<PurchaseInfo>().FromComponentInHierarchy().AsSingle();
}