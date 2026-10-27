using System.Runtime.Remoting;
using App.Domains.Entity.Api;
using App.Domains.Entity.Impl;
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
                .BindIFactory<GameObject, Transform, ISpawner>()
                .To<Spawner>()
                .AsSingle();

            Container
                .BindInterfacesTo<BootService>()
                .AsSingle()
                .WithArguments(_prefabStorage, _root);
        }
    }
}