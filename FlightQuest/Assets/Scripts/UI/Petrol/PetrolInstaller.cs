using Zenject;

public sealed class PetrolInstaller : MonoInstaller
{
    public override void InstallBindings() => Container.Bind<PetrolLevel>().FromComponentInHierarchy().AsSingle();
}