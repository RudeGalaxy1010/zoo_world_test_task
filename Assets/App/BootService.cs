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

        private readonly IFactory<Animal, Transform, ISpawner<Animal>> _spawnerFactory;
        private readonly IMoveService _moveService;
        private readonly IPrefabStorage _prefabStorage;
        private readonly IColorStorage _colorStorage;
        private readonly INameStorage _nameStorage;
        private readonly Transform _root;

        private ISpawner<Animal>? _animalSpawner;
        private Level? _level;

        public BootService(
            IFactory<Animal, Transform, ISpawner<Animal>> spawnerFactory,
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
            _level = CreateLevel();
            _animalSpawner = CreateAnimalSpawner(_level!.SpawnPoint);
        }

        public void Dispose()
        {
            if (_animalSpawner != null)
            {
                _animalSpawner.Stop();
                _animalSpawner.Spawned -= OnAnimalSpawned;
            }
        }

        private ISpawner<Animal> CreateAnimalSpawner(Transform spawnPoint)
        {
            if (_prefabStorage.TryGet(out Animal? animal))
            {
                ISpawner<Animal> animalSpawner = _spawnerFactory.Create(animal!, spawnPoint);
                animalSpawner.Start(SpawnPeriod);
                animalSpawner.Spawned += OnAnimalSpawned;
                return animalSpawner;
            }

            throw new InvalidOperationException($"{nameof(BootService)}: Animal not found");
        }

        private Level? CreateLevel() => _prefabStorage.TryGet(out Level? level)
            ? Object.Instantiate(level, _root)
            : throw new InvalidOperationException($"{nameof(BootService)}: Level not found");

        private void OnAnimalSpawned(Animal animal)
        {
            if (Random.value < PredatorSpawnChance)
            {
                SetupPredator(animal);
            }
            else
            {
                SetupPrey(animal);
            }

            _moveService.Register(animal);
        }

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
