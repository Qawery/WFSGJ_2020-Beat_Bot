using UnityEngine;


namespace Shoot
{
	public class RotateTowardsCamera : MonoBehaviour
	{
		#region Properties
		private void LateUpdate()
		{
			if (Camera.main != null)
			{
				transform.LookAt(Camera.main.transform.position, Vector3.up);
			}
		}
		#endregion
	}
}