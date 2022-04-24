using System;
using Runtime.Player;
using UnityEngine;

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
        if (Input.GetButtonDown("Fire1"))
        {
            _status.Health -= 1;
        }
    }
}
