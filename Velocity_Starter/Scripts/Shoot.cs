using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _3rdParty.Velocity_Starter.Scripts
{
    public class Shoot : MonoBehaviour
    {
        public GameObject shellPrefab;
        public Transform shellSpawnPos;
        public Transform target;
        public Transform parent;
        private float speed = 15;
        private float turnSpeed = 2;
        private bool canShoot = true;

        private void Start()
        {
            
        }

        void CanShootAgain()
        {
            canShoot = true;
        }

        void Fire()
        {
            if (canShoot)
            {
                GameObject shell = Instantiate(shellPrefab, shellSpawnPos.position, shellSpawnPos.rotation);
                shell.GetComponent<Rigidbody>().velocity = speed * transform.forward;
                canShoot = false;
                Invoke(nameof(CanShootAgain), 0.2f);
            }
        }

        private void Update()
        {

            Vector3 dir = (target.position - parent.position).normalized;
            Debug.Log(dir);
            
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
            parent.transform.rotation = Quaternion.Slerp(parent.rotation, lookRotation, turnSpeed * Time.deltaTime);

            float? angle = RotateTurret();

            if (angle != null && Vector3.Angle(dir, parent.forward) < 10) 
                Fire();
        }

        float? RotateTurret()
        {
            float? angle = CalculateAngle(true);

            if (angle != null)
            {
                transform.localEulerAngles = new Vector3(360f - (float) angle, 0f, 0f);
            }

            return angle;
        }
        
        float? CalculateAngle(bool low)
        {
            Vector3 targetDir = target.position - transform.position;
            float y = targetDir.y;
            targetDir.y = 0f;
            float x = targetDir.magnitude;
            float gravity = -Physics.gravity.y;
            float sSqr = speed * speed;
            float underTheSqrRoot = (sSqr * sSqr) - gravity * (gravity * x * x + 2 * y * sSqr);

            if (underTheSqrRoot >= 0)
            {
                float root = Mathf.Sqrt(underTheSqrRoot);
                float highAngle = sSqr + root;
                float lowAngle = sSqr - root;

                if (low)
                    return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg);
                else
                    return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);
            }

            return null;
        }
    }
}