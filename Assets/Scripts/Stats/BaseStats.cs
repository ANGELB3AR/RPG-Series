using System;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Utils;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpParticleEffect = null;
        [SerializeField] bool shouldUseModifiers = false;
        LazyValue<int> currentLevel;

        Experience experience;

        public event Action onLevelUp;

        void Awake()
        {
            experience = GetComponent<Experience>();

            currentLevel = new LazyValue<int>(GetInitialLevel);
        }

        int GetInitialLevel()
        {
            if (experience != null)
            {
                return CalculateCurrentLevel();
            }
            return startingLevel;
        }

        void Start()
        {
            currentLevel.ForceInit();
        }

        void OnEnable()
        {
            if (experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }

        void OnDisable()
        {
            experience.onExperienceGained -= UpdateLevel;
        }

        void UpdateLevel()
        {
            int newLevel = CalculateCurrentLevel();
            if (newLevel > currentLevel.value)
            {
                currentLevel.value = newLevel;
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
            return progression.GetStat(characterClass, currentLevel.value, stat);
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
            return currentLevel.value;
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