using UnityEngine;
using RPG.Core;
using GameDevTV.Saving;
using RPG.Stats;
using System;
using GameDevTV.Utils;
using UnityEngine.Events;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [Tooltip("How much player heals on level up events")]
        [SerializeField] float regenerationPercentage = 70;
        [SerializeField] TakeDamageEvent takeDamage;
        [SerializeField] UnityEvent onDie;

        [System.Serializable]
        public class TakeDamageEvent : UnityEvent<float>
        {
        }

        LazyValue<float> healthPoints;

        BaseStats baseStats;
        bool isDead = false;
        string deathAnimParam = "die";

        void Awake()
        {
            baseStats = GetComponent<BaseStats>();
            healthPoints = new LazyValue<float>(GetInitialHealth);
        }

        float GetInitialHealth()
        {
            return baseStats.GetStat(Stat.Health);
        }

        void Start()
        {
            healthPoints.ForceInit();
        }

        void OnEnable()
        {
            baseStats.onLevelUp += RegenerateHealth;
        }

        void OnDisable()
        {
            baseStats.onLevelUp -= RegenerateHealth;
        }

        // BUG: If Player is full health and levels up their health will not be 100%
        void RegenerateHealth()
        {
            float regenHealthPoints = baseStats.GetStat(Stat.Health) * (regenerationPercentage / 100);
            healthPoints.value = Mathf.Max(healthPoints.value, regenHealthPoints);
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);

            if (healthPoints.value == 0 && !isDead)
            {
                onDie.Invoke();
                Die();
                AwardExperience(instigator);
            }
            else
            {
            takeDamage.Invoke(damage);
            }
        }

        public void Heal(float healthToRestore)
        {
            healthPoints.value = Mathf.Min(healthPoints.value + healthToRestore, GetMaxHealthPoints());
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
            return healthPoints.value;
        }

        public float GetMaxHealthPoints()
        {
            return baseStats.GetStat(Stat.Health);
        }

        public float GetPercentage()
        {
            return GetFraction() * 100;
        }

        public float GetFraction()
        {
            return healthPoints.value / baseStats.GetStat(Stat.Health);
        }

        public bool IsDead()
        {
            return isDead;
        }

        public object CaptureState()
        {
            return healthPoints.value;
        }

        public void RestoreState(object state)
        {
            healthPoints.value = (float)state;

            if (healthPoints.value == 0)
            {
                Die();
            }
        }
    }
}