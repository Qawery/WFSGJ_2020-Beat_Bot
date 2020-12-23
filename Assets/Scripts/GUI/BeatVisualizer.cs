using UnityEngine;
using Zenject;

namespace WFS
{
	public class BeatVisualizer : MonoBehaviour
	{
		[SerializeField] private GameObject[] beatIndicators = null;

		[Inject]
		private void InjectionMethod(IBeatProvider beatProvider)
		{
			beatProvider.OnBeat += beatNumber =>
			{
				foreach (var beatIndicator in beatIndicators)
				{
					beatIndicator.SetActive(!beatIndicator.activeSelf);
				}
			};
		}
	}
}

