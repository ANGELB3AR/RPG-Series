using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,99)]
        [SerializeField] int currentLevel = 0;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpParticleEffect = null;
        [SerializeField] bool shouldUseModifiers = false;

        Experience experience;

        public event Action onLevelUp;

        void Awake()
        {
            experience = GetComponent<Experience>();
        }

        private void Start()
        {
            if (experience != null)
            {
                currentLevel = CalculateCurrentLevel();
                experience.onExperienceGained += UpdateLevel;
            }
        }

        void UpdateLevel()
        {
            int newLevel = CalculateCurrentLevel();
            if (newLevel > currentLevel)
            {
                currentLevel = newLevel;
                LevelUpEffect();
                onLevelUp();
            }
        }

        public float GetStat(Stat stat)
        {
            return GetBaseStat(stat) + GetAdditiveModifiers(stat) * (1 + GetPercentageModifiers(stat) / 100);
        }

        float GetBaseStat(Stat stat)
        {
            return progression.GetStat(characterClass, currentLevel, stat);
        }

        float GetAdditiveModifiers(Stat stat)
        {
            if (!shouldUseModifiers) { return 0; }

            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetAdditiveModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        float GetPercentageModifiers(Stat stat)
        {
            if (!shouldUseModifiers) { return 0; }

            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetPercentageModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        public int GetCurrentLevel()
        {
            if (currentLevel < 1)
            {
                currentLevel = CalculateCurrentLevel();
            }
            return currentLevel;
        }

        int CalculateCurrentLevel()
        {
            float currentXP = experience.GetCurrentExperience();
            int maxLevel = progression.GetLevelCount(characterClass);

            for (int level = 1; level < maxLevel + 1; level++)
            {
                float XPRequired = progression.GetStat(characterClass, level, Stat.ExperienceRequired);

                if (XPRequired > currentXP)
                {
                    return level - 1;
                }
                else if (XPRequired == currentXP)
                {
                    return level;
                }
            }
            return maxLevel;
        }

        void LevelUpEffect()
        {
            Instantiate(levelUpParticleEffect, transform);
        }
    }
}