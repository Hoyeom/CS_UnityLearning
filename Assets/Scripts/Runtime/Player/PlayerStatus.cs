using System;
using System.Collections.Generic;
using Runtime.Player;
using UnityEngine;


[CreateAssetMenu]
public class PlayerStatus : ScriptableObject, ISerializationCallbackReceiver, IPlayerStatusEvent
{
    #region VALUABLE
    
    [SerializeField] private float _maxHealth;
    
    [NonSerialized] public float MaxHealth;
    private float _health;
    public float Health
    {
        get => _health;
        set
        {
            if(_health <= 0) { return; }
            
            _health = value;
            RaiseChangedHealth(Health, MaxHealth);

            if (_health <= 0)
            {
                RaisePlayerDied();
            }
        }
    }

    #endregion
    
    #region EVENT

    
    private List<PlayerEventListener> _listeners =
        new List<PlayerEventListener>();

    private void RaisePlayerDied()
    {
        for (int i = 0; i < _listeners.Count; i++)
        { _listeners[i].OnPlayerDied(); }
    }

    private void RaiseChangedHealth(float curHealth, float maxHealth)
    {
        for (int i = 0; i < _listeners.Count; i++)
        { _listeners[i].OnChangedHealth(curHealth,maxHealth); }
    }
    
    public void RegisterListener(PlayerEventListener listener) =>
        _listeners.Add(listener);

    public void UnregisterListener(PlayerEventListener listener) =>
        _listeners.Remove(listener);
    

    #endregion

    #region INITIALIZE

    public void OnAfterDeserialize()
    {
        _health = MaxHealth = _maxHealth;
    }
    
    public void OnBeforeSerialize() { }
    

    #endregion
}
