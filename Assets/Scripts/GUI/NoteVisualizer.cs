using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace WFS
{
	public class NoteVisualizer : MonoBehaviour
	{
		[Inject] private InputMapping inputMapping = null;
		[Inject] private TextMeshProUGUI text = null;

		private Image image;
		private Color initColor;
		
		public void SetNote(Note note)
		{
			text.text = inputMapping.GetKeyForScaleDegree(note.ScaleDegree).ToString();
			image = GetComponent<Image>();
			initColor = image.color;
		}

		public void VisualizeHit()
		{
			StartCoroutine(FlashCoroutine());
		}

		public void Clear()
		{
			image.color = initColor;
		}

		IEnumerator FlashCoroutine()
		{
			image.color = Color.red;
			yield return new WaitForSeconds(1.0f);
			image.color = initColor;
		}
	}
}

