using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _3rdParty.Velocity_Starter.Scripts
{
    public class Shoot : MonoBehaviour
    {
        public GameObject shellPrefab;
        public Transform shellSpawnPos;

        private void Start()
        {
            
        }

        void Fire()
        {
            GameObject shell = Instantiate(shellPrefab, shellSpawnPos.position, shellSpawnPos.rotation);
        }

        private void Update()
        {
            if(Keyboard.current.spaceKey.wasPressedThisFrame)
                Fire();
        }
    }
}