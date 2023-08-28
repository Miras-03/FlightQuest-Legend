using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings() => 
        Container.Bind<SceneManager>().FromComponentInHierarchy().AsSingle();
}