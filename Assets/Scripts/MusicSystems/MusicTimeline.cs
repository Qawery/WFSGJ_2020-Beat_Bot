using UnityEngine.Assertions;
using UnityEngine;

namespace WFS
{
	public interface IBeatProvider
	{
		float Tempo { get; }
		int SignatureUpper { get; }
		int SignatureLower { get; } 
		float BeatDuration { get; }
		event System.Action<int> OnBeat;
		float GetBeatTimestamp(int beatsForward);
		float GetRelativeTimestampOfNote(int subdivision, int start);
	}

	public interface IMusicController
	{
		event System.Action OnMusicStarted;
	}
	
	public class MusicTimeline : MonoBehaviour, IBeatProvider, IMusicController
	{
		[SerializeField] private int signatureUpper = 4;
		[SerializeField] private int signatureLower = 4;
		[SerializeField] private float tempo = 90;
		[SerializeField] private bool playOnStart = false;
		private float beatTimer = 0.0f;
		private int beatCounter = 1;

		public float Tempo
		{
			get => tempo;
			set => tempo = value;
		}

		public int SignatureUpper
		{
			get => signatureUpper;
			set => signatureUpper = value;
		}

		public int SignatureLower
		{
			get => signatureLower;
			set => signatureLower = value;
		}

		public float BeatDuration => 60.0f / tempo;
		
		public bool IsPlaying { get; private set; }

		public event System.Action<int> OnBeat;
		public event System.Action OnMusicStarted;
		public event System.Action OnMusicStopped;

		public float GetBeatTimestamp(int beatsForward)
		{
			return Time.time + BeatDuration - beatTimer + beatsForward * BeatDuration;
		}
		
		public void StartMusic()
		{
			IsPlaying = true;
			beatTimer = 0;
			beatCounter = 1;
			OnMusicStarted?.Invoke();
			OnBeat?.Invoke(beatCounter);
		}
		
		public void StopMusic()
		{
			IsPlaying = false;
			OnMusicStopped?.Invoke();
		}
		
		/// <summary>
		/// Returns a timestamp of the start of a note
		/// </summary>
		public float GetRelativeTimestampOfNote(int subdivision, int noteStart)
		{
			float noteDuration = BeatDuration * SignatureLower / subdivision;
			return (noteStart - 1) * noteDuration;
		}

		private void Start()
		{
			if (playOnStart)
			{
				StartMusic();
			}
		}

		private void Update()
		{
			if (!IsPlaying)
			{
				return;
			}
			
			beatTimer += Time.deltaTime;
			if (beatTimer > BeatDuration)
			{
				beatCounter++;
				if (beatCounter > signatureLower)
				{
					beatCounter = 1;
				}
				OnBeat?.Invoke(beatCounter);
				beatTimer -= BeatDuration;
			}
		}
	}
}
