using UnityEngine;

namespace Tests
{
    public class InjectedComponentOnGO : MonoBehaviour
    {
        public static int CallCounter;
        public void Method()
        {
            Debug.Log($"Calling {nameof(Method)} of {GetType().Name}");
            CallCounter++;
        }
    }
}
