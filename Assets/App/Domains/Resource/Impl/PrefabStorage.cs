using System;
using App.Domains.Resource.Api;
using UnityEngine;

namespace App.Domains.Resource.Impl
{
    [CreateAssetMenu(fileName = nameof(PrefabStorage), menuName = MenuConstants.Storage + "/" + nameof(PrefabStorage))]
    public class PrefabStorage : ScriptableObject, IPrefabStorage
    {
        [SerializeField] private GameObject[] _prefabs = Array.Empty<GameObject>();

        public bool TryGet<T>(out T? prefab)
        {
            for (int i = 0; i < _prefabs.Length; i++)
            {
                if (_prefabs[i].TryGetComponent(out prefab))
                {
                    return true;
                }
            }

            prefab = default;
            return false;
        }
    }
}