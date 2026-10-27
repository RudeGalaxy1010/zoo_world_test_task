using System.Collections.Generic;
using App.Domains.Entity.Api;
using App.Domains.Entity.Impl;
using App.Domains.Resource.Api;
using UnityEngine;
using Zenject;

namespace App
{
    public class BootService : IInitializable
    {
        private readonly IFactory<GameObject, Transform, ISpawner> _spawnerFactory;
        private readonly IPrefabStorage _prefabStorage;
        private readonly Transform _root;

        private Level? _level;
        private ISpawner[] _spawners = new ISpawner[1];

        public BootService(
            IFactory<GameObject, Transform, ISpawner> spawnerFactory,
            IPrefabStorage prefabStorage,
            Transform root)
        {
            _spawnerFactory = spawnerFactory;
            _prefabStorage = prefabStorage;
            _root = root;
        }

        public void Initialize()
        {
            CreateLevel();
            CreateAnimalSpawner();

            float spawnPeriod = 2f;
            _spawners[0].Start(spawnPeriod);
        }

        private void CreateAnimalSpawner()
        {
            if (_prefabStorage.TryGet(out Animal? animal))
            {
                _spawners[0] = _spawnerFactory.Create(animal!.gameObject, _root);
            }
            else
            {
                Debug.LogError($"{nameof(BootService)}: Animal not found");
            }
        }

        private void CreateLevel()
        {
            if (_prefabStorage.TryGet(out Level? level))
            {
                _level = Object.Instantiate(level, _root);
            }
            else
            {
                Debug.LogError($"{nameof(BootService)}: Level not found");
            }
        }
    }
}