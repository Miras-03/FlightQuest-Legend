using UnityEngine;
using Zenject;

public sealed class AudioInstaller : MonoInstaller
{
    [SerializeField] private AudioSource[] sounds;
    public override void InstallBindings()
    {
        foreach (AudioSource sound in sounds)
            Container.BindInstance(sound);
    }
}