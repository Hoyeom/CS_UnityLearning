using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime
{
    [RequireComponent(typeof(FaceCamera))]
    public class HealthBarUI : MonoBehaviour
    {
        [SerializeField] private Image healthImage;
        [SerializeField] private bool isDOTween;
        [SerializeField] private float doTime;
        
        private void OnValidate()
        {
            healthImage ??= transform.Find("Health Bar Image").GetComponent<Image>();
        }

        public void SetHealthBar(float curValue, float maxValue)
        {
            if(!isDOTween) healthImage.fillAmount = curValue / maxValue;
            else healthImage.DOFillAmount(curValue / maxValue, doTime).Restart();
        }
    }
}