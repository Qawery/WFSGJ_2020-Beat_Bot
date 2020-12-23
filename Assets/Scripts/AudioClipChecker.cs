using UnityEngine;
using Zenject;


namespace WFS
{
	public class AudioClipChecker : MonoBehaviour
	{
		[Inject]
		private void InjectionMethod(IBeatProvider beatProvider, AudioSource audioSource)
		{
			int samplesPerBeat = (int) (beatProvider.BeatDuration * audioSource.clip.frequency);
			Debug.Log($"Samples: {samplesPerBeat} Diff: {audioSource.clip.samples % samplesPerBeat}");
		}
	}
}
