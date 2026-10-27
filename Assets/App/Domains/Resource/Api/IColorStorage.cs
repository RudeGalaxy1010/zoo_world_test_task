using UnityEngine;

namespace App.Domains.Resource.Api
{
    public interface IColorStorage
    {
        Color PredatorColor { get; }
        Color PreyColor { get; }
    }
}