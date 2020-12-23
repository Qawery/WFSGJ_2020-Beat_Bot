using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;


namespace WFS
{
    public class HealthBar : MonoBehaviour
    {
        private Slider slider = null;
        private UnitComponent unitComponent = null;


        private void Awake()
        {
            slider = GetComponentInChildren<Slider>();
            Assert.IsNotNull(slider);
            unitComponent = GetComponentInParent<UnitComponent>();
            Assert.IsNotNull(unitComponent);
        }

        private void Start()
        {
            unitComponent.Unit.OnHealed += UpdateBar;
            unitComponent.Unit.OnDamaged += UpdateBar;
            UpdateBar();
        }

        private void UpdateBar()
        {
            slider.normalizedValue = unitComponent.Unit.HealthPercentage;
        }
    }
}