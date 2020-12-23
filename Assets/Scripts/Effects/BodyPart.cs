using UnityEngine;
using UnityEngine.Assertions;


namespace WFS
{
	public class BodyPart : MonoBehaviour
	{
		private MeshRenderer meshRenderer = null;
		private new Rigidbody rigidbody = null;


		public MeshRenderer MeshRenderer => meshRenderer;
		public Rigidbody Rigidbody => rigidbody;


		private void Awake()
		{
			meshRenderer = GetComponent<MeshRenderer>();
			Assert.IsNotNull(meshRenderer);
			rigidbody = GetComponent<Rigidbody>();
			Assert.IsNotNull(rigidbody);
		}
	}
}