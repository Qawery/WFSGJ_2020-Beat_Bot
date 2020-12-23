using UnityEngine;
using Zenject;


namespace WFS
{
	public class SynthController : MonoBehaviour
	{
		[Inject] private Synthesizer synth = null;
		[Inject] private InputMapping inputMapping = null;
		
		private void Update()
		{
			for (int scaleDegree = 1; scaleDegree <= 8; ++scaleDegree)
			{
				KeyCode scaleDegreeKey = inputMapping.GetKeyForScaleDegree(scaleDegree);
				if (Input.GetKeyDown(scaleDegreeKey))
				{
					synth.PlayScaleDegree(scaleDegree);
				}
			}
		}
	}
}

