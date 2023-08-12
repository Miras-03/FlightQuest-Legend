using Zenject;

namespace PlaneSection
{
    public class AccelerationInstaller : MonoInstaller
    {
        public override void InstallBindings() => Container.Bind<PlaneLevelAcceleration>().AsSingle();
    }
}