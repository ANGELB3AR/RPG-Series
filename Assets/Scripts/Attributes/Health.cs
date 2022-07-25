using UnityEngine;
using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using System;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [Tooltip("How much player heals on level up events")]
        [SerializeField] float regenerationPercentage = 70;

        float healthPoints = -1f;
        BaseStats baseStats;
        bool isDead = false;
        string deathAnimParam = "die";

        void Awake()
        {
            baseStats = GetComponent<BaseStats>();
        }

        void Start()
        {
            baseStats.onLevelUp += RegenerateHealth;

            if (healthPoints < 0)
            {
                healthPoints = baseStats.GetStat(Stat.Health);
            }
        }

        void RegenerateHealth()
        {
            float regenHealthPoints = baseStats.GetStat(Stat.Health) * (regenerationPercentage / 100);
            healthPoints = Mathf.Max(healthPoints, regenHealthPoints);
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            print($"{gameObject.name} took damage: {damage}");

            healthPoints = Mathf.Max(healthPoints - damage, 0);

            if (healthPoints == 0 && !isDead)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) { return; }

            experience.GainExperience(baseStats.GetStat(Stat.ExperienceReward));
        }

        void Die()
        {
            if (isDead) { return; }

            GetComponent<Animator>().SetTrigger(deathAnimParam);
            GetComponent<ActionScheduler>().CancelCurrentAction();
            isDead = true;
        }

        public float GetHealthPoints()
        {
            return healthPoints;
        }

        public float GetMaxHealthPoints()
        {
            return baseStats.GetStat(Stat.Health);
        }

        public float GetPercentage()
        {
            return Mathf.Round(100 * healthPoints / baseStats.GetStat(Stat.Health));
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