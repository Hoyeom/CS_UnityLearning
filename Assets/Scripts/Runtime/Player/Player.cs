using System;
using Runtime.Player;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerStatusContainer))]
public class Player : MonoBehaviour
{
    private PlayerStatus _status;
    
    
    
    private void Awake()
    {
        _status ??= GetComponent<PlayerStatusContainer>().Status;
    }

    private void Update()
    {
        // TEST
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _status.Health -= 1;
        }
    }
}
