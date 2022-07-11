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

        Transform target;
        Mover mover;
        ActionScheduler actionScheduler;
        Animator animator;
        string attackAnimParam = "attack";
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

            if (!GetIsInRange())
            {
                mover.MoveTo(target.position);
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
                animator.SetTrigger(attackAnimParam);
                timeSinceLastAttack = 0;
            }
        }

        bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        // Called from PlayerController script
        public void Attack(CombatTarget combatTarget)
        {
            actionScheduler.StartAction(this);
            target = combatTarget.transform;
        }

        // Called from ActionScheduler script
        public void Cancel()
        {
            target = null;
        }

        // Called from Animation Event
        void Hit()
        {

        }
    }
}