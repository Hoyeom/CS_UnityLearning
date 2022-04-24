using System;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime
{
    [RequireComponent(typeof(FaceCamera))]
    public class HealthBarUI : MonoBehaviour
    {
        [SerializeField] private Image healthImage;
        private void OnValidate()
        {
            healthImage ??= transform.Find("Health Bar Image").GetComponent<Image>();
        }

        public void SetHealthBar(float curValue, float maxValue)
        {
            healthImage.fillAmount = curValue / maxValue;
        }
    }
}