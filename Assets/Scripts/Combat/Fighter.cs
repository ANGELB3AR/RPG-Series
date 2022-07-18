using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon defaultWeapon = null;

        Health target;
        Mover mover;
        ActionScheduler actionScheduler;
        Animator animator;
        string attackAnimParam = "attack";
        string stopAttackAnimParam = "stopAttack";
        float timeSinceLastAttack = Mathf.Infinity;
        Weapon currentWeapon = null;

        void Awake()
        {
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
        }

        void Start()
        {
            EquipWeapon(defaultWeapon);
        }

        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) { return; }
            if (target.IsDead()) { return; }

            if (!GetIsInRange())
            {
                mover.MoveTo(target.transform.position, 1f);
            }
            else
            {
                mover.Cancel();
                AttackBehavior();
            }
        }

        void AttackBehavior()
        {
            transform.LookAt(target.transform);

            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                // This will trigger the Hit() event
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        void TriggerAttack()
        {
            animator.ResetTrigger(stopAttackAnimParam);
            animator.SetTrigger(attackAnimParam);
        }

        bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetRange();
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) { return false; }

            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        // Called from PlayerController script
        public void Attack(GameObject combatTarget)
        {
            actionScheduler.StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        // Called from ActionScheduler script
        public void Cancel()
        {
            StopAttack();
            target = null;
            actionScheduler.CancelCurrentAction();
        }

        void StopAttack()
        {
            animator.ResetTrigger(attackAnimParam);
            animator.SetTrigger(stopAttackAnimParam);
        }

        // Called from Animation Event
        void Hit()
        {
            if (target == null) { return; }

            if (currentWeapon.HasProjectile())
            {
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target);
            }
            else
            {
                target.TakeDamage(currentWeapon.GetDamage());
            }
        }

        // Called from Animation Event
        void Shoot()
        {
            Hit();
        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }
    }
}