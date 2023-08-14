using PlaneSection;
using UnityEngine;
using Zenject;

public sealed class PlaneStateInstaller : MonoInstaller
{
    [SerializeField] private GroundState groundState;
    public override void InstallBindings() => Container.Bind<IPlaneState>().FromInstance(groundState).AsSingle();
}