using UnityEngine;
using UnityEngine.Assertions;


namespace WFS
{
	public class UnitAnimationController : MonoBehaviour
	{
		public const string IDLE = "Idle";
		public const string ATTACK = "Attack";
		public const string DEFENCE = "Defence";

		private Animator animator = null;
		private UnitComponent unitComponent = null;
		private string currentAnimationFlag = IDLE;
		private const float ANIMATION_TIME = 0.5f;
		private float animationTimer = ANIMATION_TIME;


		private string CurrentAnimationFlag
		{
			get => currentAnimationFlag;
			set
			{
				animator.SetBool(currentAnimationFlag, false);
				currentAnimationFlag = value;
				animator.SetBool(currentAnimationFlag, true);
				animationTimer = ANIMATION_TIME;
			}
		}


		private void Awake()
		{
			animator = GetComponent<Animator>();
			Assert.IsNotNull(animator);
			unitComponent = GetComponentInParent<UnitComponent>();
			Assert.IsNotNull(unitComponent);
			CurrentAnimationFlag = IDLE;
		}

		private void Start()
		{
			unitComponent.Unit.OnActionExecuted += OnActionExecuted;
		}

		private void Update()
		{
			if (animationTimer > 0.0f)
			{
				animationTimer -= Time.deltaTime;
			}
			else
			{
				CurrentAnimationFlag = IDLE;
			}
		}

		private void OnActionExecuted(CombatAction combatAcion)
		{
			CurrentAnimationFlag = combatAcion.AnimationName;
		}
	}
}