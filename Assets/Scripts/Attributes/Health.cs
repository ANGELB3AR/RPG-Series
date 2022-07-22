using UnityEngine;
using RPG.Core;
using RPG.Saving;
using RPG.Stats;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100f;

        bool isDead = false;
        string deathAnimParam = "die";

        void Start()
        {
            healthPoints = GetComponent<BaseStats>().GetHealth();
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            print(healthPoints);

            if (healthPoints == 0 && !isDead)
            {
                Die();
            }
        }

        void Die()
        {
            if (isDead) { return; }

            GetComponent<Animator>().SetTrigger(deathAnimParam);
            GetComponent<ActionScheduler>().CancelCurrentAction();
            isDead = true;
        }

        public bool IsDead()
        {
            return isDead;
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;

            if (healthPoints == 0)
            {
                Die();
            }
        }
    }
}