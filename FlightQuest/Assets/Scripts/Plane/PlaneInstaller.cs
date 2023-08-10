using PlaneSection;
using UnityEngine;
using Zenject;

public class PlaneInstaller : MonoInstaller
{
    [SerializeField] private LightPlane lightGroundPlane;
    public override void InstallBindings() => Container.Bind<Plane>().FromInstance(lightGroundPlane).AsSingle();
}