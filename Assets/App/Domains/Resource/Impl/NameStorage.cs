using System;
using App.Domains.Resource.Api;
using UnityEngine;

namespace App.Domains.Resource.Impl
{
    [CreateAssetMenu(fileName = nameof(NameStorage), menuName = MenuConstants.Storage + "/" + nameof(NameStorage))]
    public class NameStorage : ScriptableObject, INameStorage
    {
        [SerializeField] private string[] _predatorNames = Array.Empty<string>();
        [SerializeField] private string[] _preyNames = Array.Empty<string>();

        public string[] PredatorNames => _predatorNames;
        public string[] PreyNames => _preyNames;
    }
}