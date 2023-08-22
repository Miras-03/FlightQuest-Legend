using Zenject;

public class NotifyInstaller : MonoInstaller
{
    public override void InstallBindings() => 
        Container.Bind<PrefabInitializationNotifier>().FromComponentInHierarchy().AsSingle();
}
