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
        [SerializeField] private ColorStorage _colorStorage = null!;
        [SerializeField] private NameStorage _nameStorage = null!;

        public override void InstallBindings()
        {
            Container
                .BindIFactory<Animal, Transform, ISpawner<Animal>>()
                .To<Spawner<Animal>>()
                .AsSingle();

            Container.BindInterfacesTo<MoveService>().AsSingle();

            Container
                .BindInterfacesTo<BootService>()
                .AsSingle()
                .WithArguments(_prefabStorage, _colorStorage, _nameStorage, _root);
        }
    }
}