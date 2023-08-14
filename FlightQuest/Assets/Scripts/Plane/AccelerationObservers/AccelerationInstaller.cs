using Zenject;

namespace PlaneSection
{
    public sealed class AccelerationInstaller : MonoInstaller
    {
        public override void InstallBindings() => Container.Bind<PlaneLevelAcceleration>().AsSingle();
    }
}