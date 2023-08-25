using Zenject;

public sealed class UIInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<UIManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<UIAnimationManager>().FromComponentInHierarchy().AsSingle();
    }
}