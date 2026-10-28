using App.Domains.Entity.Impl;
using App.Domains.Resource.Api;
using UnityEngine;

namespace App.Domains.Resource.Impl
{
    [CreateAssetMenu(fileName = nameof(PrefabStorage), menuName = MenuConstants.Storage + "/" + nameof(PrefabStorage))]
    public class PrefabStorage : ScriptableObject, IPrefabStorage
    {
        [SerializeField] private Level _level = null!;
        [SerializeField] private Animal _frog = null!;
        [SerializeField] private Animal _snake = null!;

        public Level Level => _level;
        public Animal Frog => _frog;
        public Animal Snake => _snake;
    }
}
