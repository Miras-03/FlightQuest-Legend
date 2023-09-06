using Zenject;

public sealed class JoystickInstaller : MonoInstaller
{
    public override void InstallBindings() => 
        Container.Bind<FixedJoystick>().FromComponentInHierarchy().AsSingle();
}
