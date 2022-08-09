using UnityEngine;
using RPG.Attributes;
using UnityEngine.Events;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float projectileSpeed = 1f;
        [SerializeField] bool isHoming = false;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float maxLifetime = 10f;
        [SerializeField] UnityEvent onHit;
        
        Health target = null;
        GameObject instigator = null;
        float damage = 0f;

        void Start()
        {
            if (target == null) { return; }
            AimAtTarget();
        }

        void Update()
        {
            if (target == null) { return; }

            if (isHoming && !target.IsDead()) { AimAtTarget(); }

            FireProjectile();
        }

        public void SetTarget(Health target,GameObject instigator, float damage)
        {
            this.target = target;
            this.damage = damage;
            this.instigator = instigator;

            Destroy(gameObject, maxLifetime);
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
            if (target.IsDead()) { return; }

            target.TakeDamage(instigator, damage);

            onHit.Invoke();

            if (hitEffect != null)
            {
                Instantiate(hitEffect, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}