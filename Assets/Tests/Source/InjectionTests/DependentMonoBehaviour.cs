using UnityEngine;
using Zenject;

namespace Tests
{
    public class DependentMonoBehaviour : MonoBehaviour
    {
        [Inject] private InjectedComponent injectedComponent = null;
        [Inject] private InjectedPureClass pureClass = null;

        public void Method()
        {
            Debug.Log($"Calling {nameof(Method)} of {GetType().Name}");
            injectedComponent.Method();
            pureClass.Method();
        }
    }
}
