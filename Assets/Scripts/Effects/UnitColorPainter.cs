using UnityEngine;


namespace WFS
{
	public class UnitColorPainter : MonoBehaviour
	{
		public void ColorWithMaterial(Material materialToColor)
		{
			foreach (var bodyPart in GetComponentsInChildren<BodyPart>())
			{
				bodyPart.MeshRenderer.material = materialToColor;
			}
		}
	}
}