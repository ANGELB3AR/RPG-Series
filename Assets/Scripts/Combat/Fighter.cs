using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamange = 5f;

        Health target;
        Mover mover;
        ActionScheduler actionScheduler;
        Animator animator;
        string attackAnimParam = "attack";
        string stopAttackAnimParam = "stopAttack";
        float timeSinceLastAttack = 0;

        void Awake()
        {
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) { return; }
            if (target.IsDead()) { return; }

            if (!GetIsInRange())
            {
                mover.MoveTo(target.transform.position);
            }
            else
            {
                mover.Cancel();
                AttackBehavior();
            }
        }

        void AttackBehavior()
        {
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                // This will trigger the Hit() event
                animator.SetTrigger(attackAnimParam);
                timeSinceLastAttack = 0;
            }
        }

        bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        // Called from PlayerController script
        public void Attack(CombatTarget combatTarget)
        {
            actionScheduler.StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        // Called from ActionScheduler script
        public void Cancel()
        {
            animator.SetTrigger(stopAttackAnimParam);
            target = null;
        }

        // Called from Animation Event
        void Hit()
        {
            target.TakeDamage(weaponDamange);
        }
    }
}