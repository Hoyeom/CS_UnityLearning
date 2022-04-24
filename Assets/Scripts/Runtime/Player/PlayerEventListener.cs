using System;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Player
{
    [RequireComponent(typeof(PlayerStatusContainer))]
    public class PlayerEventListener : MonoBehaviour
    {
        private IPlayerStatusEvent _statusEvent;
        public UnityEvent onPlayerDied;
        public UnityEvent<float, float> onChangedHealth;

        private void Awake()
        { _statusEvent ??= GetComponent<PlayerStatusContainer>().StatusEvent; }

        private void OnEnable()
        { _statusEvent.RegisterListener(this); }

        private void OnDisable()
        { _statusEvent.UnregisterListener(this); }

        public void OnChangedHealth(float curHealth, float maxHealth)
        { onChangedHealth?.Invoke(curHealth, maxHealth); }
        
        public void OnPlayerDied()
        { onPlayerDied?.Invoke(); }
    }
}