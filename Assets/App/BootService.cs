using App.Domains.Entity.Impl;
using App.Domains.Resource.Api;
using UnityEngine;
using Zenject;

namespace App
{
    public class BootService : IInitializable
    {
        private readonly IPrefabStorage _prefabStorage;
        private readonly Transform _root;

        private Level? _level;

        public BootService(IPrefabStorage prefabStorage, Transform root)
        {
            _prefabStorage = prefabStorage;
            _root = root;
        }

        public void Initialize()
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