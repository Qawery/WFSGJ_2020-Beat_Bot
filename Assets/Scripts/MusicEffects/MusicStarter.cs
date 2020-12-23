using UnityEngine;
using Zenject;

namespace WFS
{
	public class MusicStarter : MonoBehaviour
	{
		[Inject]
		private void InjectionMethod(IMusicController musicController, AudioSource audioSource)
		{
			musicController.OnMusicStarted += audioSource.Play;
		}
	}

}
