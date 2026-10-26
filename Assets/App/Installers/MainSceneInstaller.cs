using UnityEngine;
using Zenject;

namespace App.Installers
{
    [CreateAssetMenu(fileName = "MainSceneInstaller", menuName = "Installers/MainSceneInstaller")]
    public class MainSceneInstaller : ScriptableObjectInstaller<MainSceneInstaller>
    {
        public override void InstallBindings() { }
    }
}