using UnityEngine;
using Zenject;

public class JoystickInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<FixedJoystick>().FromComponentInHierarchy().AsSingle();
    }
}
