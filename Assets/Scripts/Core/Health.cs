using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;

        bool isDead = false;
        string deathAnimParam = "die";

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            print(healthPoints);

            if (healthPoints == 0 && !isDead)
            {
                GetComponent<Animator>().SetTrigger(deathAnimParam);
                GetComponent<ActionScheduler>().CancelCurrentAction();
                isDead = true;
            }
        }

        public bool IsDead()
        {
            return isDead;
        }
    }
}