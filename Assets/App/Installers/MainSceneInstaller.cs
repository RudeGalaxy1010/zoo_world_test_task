using App.Domains.Resource.Impl;
using UnityEngine;
using Zenject;

namespace App.Installers
{
    public class MainSceneInstaller : MonoInstaller<MainSceneInstaller>
    {
        [SerializeField] private Transform _root = null!;
        [SerializeField] private PrefabStorage _prefabStorage = null!;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<BootService>()
                .AsSingle()
                .WithArguments(_prefabStorage, _root);
        }
    }
}