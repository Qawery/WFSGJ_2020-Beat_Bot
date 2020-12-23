using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class InjectionTests
    {
        [SetUp]
        public void LoadScene()
        {
            SceneManager.LoadScene("InjectionTestScene");
        }
        
        [UnityTest]
        public IEnumerator RunTestScene()
        {
            var dependentComponent = Object.FindObjectOfType<DependentMonoBehaviour>();
            dependentComponent.Method();
            Assert.IsTrue(InjectedPureClass.CallCounter == 1);
            Assert.IsTrue(InjectedComponent.CallCounter == 1);
            Assert.IsTrue(InjectedComponentOnGO.CallCounter == InjectedPureClass.InjectedComponentMethodCalls);
            yield return null;
        }
    }
}
