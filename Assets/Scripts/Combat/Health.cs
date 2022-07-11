using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;

        bool isDead = false;
        public bool IsDead() { return isDead; }

        string deathAnimParam = "die";

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            print(healthPoints);

            if (healthPoints == 0 && !isDead)
            {
                GetComponent<Animator>().SetTrigger(deathAnimParam);
                isDead = true;
            }
        }
    }
}