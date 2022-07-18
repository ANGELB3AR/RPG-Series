using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] Transform target = null;
        [SerializeField] float projectileSpeed = 1f;

        void Update()
        {
            if (target == null) { return; }

            AimAtTarget();
            FireProjectile();
        }

        void AimAtTarget()
        {
            transform.LookAt(GetAimLocation());
        }

        Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null)
            {
                return target.position;
            }
            return target.position + Vector3.up * (targetCapsule.height / 2); // Offset to aim toward middle of body
        }

        void FireProjectile()
        {
            transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
        }
    }
}