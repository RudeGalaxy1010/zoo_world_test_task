using App.Domains.Resource.Api;
using UnityEngine;

namespace App.Domains.Resource.Impl
{
    [CreateAssetMenu(fileName = nameof(ColorStorage), menuName = MenuConstants.Storage + "/" + nameof(ColorStorage))]
    public class ColorStorage : ScriptableObject, IColorStorage
    {
        [SerializeField] private Color _predatorColor;
        [SerializeField] private Color _preyColor;

        public Color PredatorColor => _predatorColor;
        public Color PreyColor => _preyColor;
    }
}