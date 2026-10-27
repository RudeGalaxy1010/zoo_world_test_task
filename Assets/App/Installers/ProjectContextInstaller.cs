using App.Domains.Core.Impl;
using UnityEngine;
using Zenject;

namespace App.Installers
{
    [CreateAssetMenu(fileName = "ProjectContextInstaller", menuName = "Installers/ProjectContextInstaller")]
    public class ProjectContextInstaller : ScriptableObjectInstaller<ProjectContextInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<TimeProvider>().AsSingle();
            Container.BindInterfacesTo<TimerService>().AsSingle();
        }
    }
}