using Zenject;

public class CrystalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Crystal>().AsSingle();
        Container.Bind<CrystalManager>().FromComponentInHierarchy().AsSingle();
    }
}