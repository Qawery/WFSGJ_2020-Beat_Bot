using UnityEngine;
using Zenject;

namespace WFS
{
	public class UnitParticles : MonoBehaviour
	{
		[SerializeField] private ParticleSystem hitParticles = null;
		[SerializeField] private ParticleSystem healParticles = null;

		[Inject]
		private void InjectionMethod(UnitComponent unitComponent)
		{
			unitComponent.Unit.OnDamaged += OnDamaged;		
			unitComponent.Unit.OnHealed += OnHealed;
		}
		
		private void OnDamaged()
		{
		}

		private void OnHealed()
		{
		}
	}
}
