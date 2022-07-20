using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float projectileSpeed = 1f;
        [SerializeField] bool isHoming = false;
        
        Health target = null;
        float damage = 0f;

        void Start()
        {
            if (target == null) { return; }
            AimAtTarget();
        }

        void Update()
        {
            if (target == null) { return; }

            if (isHoming) { AimAtTarget(); }

            FireProjectile();
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
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
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * (targetCapsule.height / 2); // Offset to aim toward middle of body
        }

        void FireProjectile()
        {
            transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target) { return; }

            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}