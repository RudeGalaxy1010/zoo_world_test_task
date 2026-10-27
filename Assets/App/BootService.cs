using System;
using App.Domains.Entity.Api;
using App.Domains.Entity.Impl;
using App.Domains.Resource.Api;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace App
{
    public class BootService : IInitializable, IDisposable
    {
        private const float SpawnPeriod = 2f;

        private readonly IFactory<Animal, Transform, ISpawner<Animal>> _spawnerFactory;
        private readonly IPrefabStorage _prefabStorage;
        private readonly IColorStorage _colorStorage;
        private readonly Transform _root;

        private ISpawner<Animal>? _animalSpawner;
        private Level? _level;

        public BootService(
            IFactory<Animal, Transform, ISpawner<Animal>> spawnerFactory,
            IPrefabStorage prefabStorage,
            IColorStorage colorStorage,
            Transform root)
        {
            _spawnerFactory = spawnerFactory;
            _prefabStorage = prefabStorage;
            _colorStorage = colorStorage;
            _root = root;
        }

        public void Initialize()
        {
            CreateLevel();
            CreateAnimalSpawner();
        }

        private void CreateAnimalSpawner()
        {
            if (_prefabStorage.TryGet(out Animal? animal))
            {
                _animalSpawner = _spawnerFactory.Create(animal!, _root);
                _animalSpawner.Start(SpawnPeriod);
                _animalSpawner.Spawned += OnAnimalSpawned;
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

        private void OnAnimalSpawned(Animal animal) =>
            animal.SetColor(Random.value < 0.5f
                ? _colorStorage.PredatorColor
                : _colorStorage.PreyColor);

        public void Dispose()
        {
            if (_animalSpawner != null)
            {
                _animalSpawner.Stop();
                _animalSpawner.Spawned -= OnAnimalSpawned;
            }
        }
    }
}