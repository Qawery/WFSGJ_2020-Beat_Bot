using UnityEngine;
using WFS;
using Zenject;


namespace WTS
{
	public class Metronome : MonoBehaviour
	{
		[SerializeField] private AudioClip audioClip = null;
		private AudioSource audioSource = null;

		[Inject]
		private void InjectionMethod(IBeatProvider beatProvider, AudioSource audioSource)
		{
			this.audioSource = audioSource;
			beatProvider.OnBeat += OnBeat;
		}

		private void OnBeat(int beatNumber)
		{
			audioSource.PlayOneShot(audioClip);
		}
	}
}