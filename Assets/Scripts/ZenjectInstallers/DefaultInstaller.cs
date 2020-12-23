using TMPro;
using UnityEngine;
using Zenject;


namespace WFS
{
	[CreateAssetMenu(menuName = "ZenjectInstallers/DefaultInstaller", fileName = "DefaultInstaller")]
	public class DefaultInstaller : ScriptableObjectInstaller
	{
		[SerializeField] private PlayerController playerController = null;
		[SerializeField] private ActionMapper actionMapper = null;
		
		public override void InstallBindings()
		{
			Container.Bind<AudioSource>().FromComponentInChildren();
			Container.Bind<NoteSequenceSet>().FromComponentInChildren();
			Container.Bind<IBeatProvider>().To<MusicTimeline>().FromComponentInHierarchy().AsCached();
			Container.Bind<IMusicController>().To<MusicTimeline>().FromComponentInHierarchy().AsCached();
			Container.Bind<INoteSequenceChecker>().FromComponentsInChildren();
			Container.Bind<IWorld>().To<World>().FromComponentInHierarchy().AsSingle();
			Container.Bind<InputMapping>().AsSingle();
			Container.Bind<PlayerController>().FromComponentInNewPrefab(playerController).AsSingle();
			Container.Bind<ActionMapper>().FromScriptableObject(actionMapper).AsSingle();
			Container.Bind<Board>().FromComponentInHierarchy().AsSingle();
			Container.Bind<UnitComponent>().FromComponentInChildren();

			Container.Bind<TextMeshProUGUI>().FromComponentInChildren();
			Container.Bind<Synthesizer>().FromComponentInChildren();
		}
	}
}
