using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float weaponRange = 2f;

        Transform target;
        Mover mover;

        void Awake()
        {
            mover = GetComponent<Mover>();
        }

        void Update()
        {
            if (target != null && GetIsInRange())
            {
                mover.MoveTo(target.position);
            }
            else
            {
                mover.Stop();
            }
        }

        bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        // Called from PlayerController script
        public void Attack(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
        }

        // Called from Mover script
        public void CancelAttack()
        {
            target = null;
        }
    }
}