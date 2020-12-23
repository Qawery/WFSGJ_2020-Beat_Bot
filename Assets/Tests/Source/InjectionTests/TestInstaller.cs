using UnityEngine;

namespace Tests
{
    public class TestInstaller : Zenject.MonoInstaller
    {
        [SerializeField] private InjectedComponentOnGO prefab = null;
        public override void InstallBindings()
        {
            Container.Bind<InjectedComponent>().FromComponentInChildren().AsTransient();
            Container.Bind<InjectedComponentOnGO>().FromComponentInNewPrefab(prefab).AsSingle();
            Container.Bind<InjectedPureClass>().AsSingle();
        }
    }
}