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
        private const float PredatorSpawnChance = 0.5f;

        private readonly IFactory<Func<Animal>, ISpawner<Animal>> _spawnerFactory;
        private readonly IMoveService _moveService;
        private readonly IPrefabStorage _prefabStorage;
        private readonly IColorStorage _colorStorage;
        private readonly INameStorage _nameStorage;
        private readonly Transform _root;

        private ISpawner<Animal>? _animalSpawner;
        private Level? _level;

        public BootService(
            IFactory<Func<Animal>, ISpawner<Animal>> spawnerFactory,
            IMoveService moveService,
            IPrefabStorage prefabStorage,
            IColorStorage colorStorage,
            INameStorage nameStorage,
            Transform root)
        {
            _spawnerFactory = spawnerFactory;
            _moveService = moveService;
            _prefabStorage = prefabStorage;
            _colorStorage = colorStorage;
            _nameStorage = nameStorage;
            _root = root;
        }

        public void Initialize()
        {
            if (_prefabStorage.Level == null || _prefabStorage.Frog == null || _prefabStorage.Snake == null)
            {
                throw new InvalidOperationException($"{nameof(BootService)}: Level, Frog and Snake prefabs must be assigned");
            }

            _level = Object.Instantiate(_prefabStorage.Level, _root);
            _animalSpawner = _spawnerFactory.Create(CreateAnimal);
            _animalSpawner.Spawned += OnAnimalSpawned;
            _animalSpawner.Start(SpawnPeriod);
        }

        public void Dispose()
        {
            if (_animalSpawner != null)
            {
                _animalSpawner.Stop();
                _animalSpawner.Spawned -= OnAnimalSpawned;
            }
        }

        private Animal CreateAnimal()
        {
            bool isPredator = Random.value < PredatorSpawnChance;
            Animal prefab = isPredator ? _prefabStorage.Snake : _prefabStorage.Frog;
            Animal animal = Object.Instantiate(prefab, _level!.SpawnPoint);
            if (isPredator)
            {
                SetupPredator(animal);
            }
            else
            {
                SetupPrey(animal);
            }

            return animal;
        }

        private void OnAnimalSpawned(Animal animal) =>
            _moveService.Register(animal);

        private void SetupPrey(Animal animal)
        {
            int nameIndex = Random.Range(0, _nameStorage.PreyNames.Length);
            animal.SetName(_nameStorage.PreyNames[nameIndex]);
            animal.SetColor(_colorStorage.PreyColor);
        }

        private void SetupPredator(Animal animal)
        {
            int nameIndex = Random.Range(0, _nameStorage.PredatorNames.Length);
            animal.SetName(_nameStorage.PredatorNames[nameIndex]);
            animal.SetColor(_colorStorage.PredatorColor);
        }
    }
}
