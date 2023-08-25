using Zenject;

public sealed class NotifyInstaller : MonoInstaller
{
    public override void InstallBindings() => 
        Container.Bind<PrefabInitializationNotifier>().FromComponentInHierarchy().AsSingle();
}
