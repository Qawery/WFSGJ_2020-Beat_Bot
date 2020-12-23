using UnityEngine;

namespace Tests
{
	public class InjectedPureClass
	{
		public const int InjectedComponentMethodCalls = 5;
		public static int CallCounter;
		public static int ConstructionCounter;
		private InjectedComponentOnGO componentOnGO;

		public InjectedPureClass(InjectedComponentOnGO componentOnGO)
		{
			this.componentOnGO = componentOnGO;
			ConstructionCounter++;
		}

		public void Method()
		{
			Debug.Log($"Calling {nameof(Method)} of {GetType().Name}");
			for (int i = 0; i < InjectedComponentMethodCalls; i++)
			{
				componentOnGO.Method();
			}

			CallCounter++;
		}
	}
}