using System;
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

        public float GetHealth()
        {
            return progression.GetHealth(characterClass, currentLevel);
        }

        public float GetAttackDamage()
        {
            return progression.GetAttackDamage(characterClass, currentLevel) + GetAdditiveModifier();
        }

        public float GetExperienceReward()
        {
            return progression.GetExperienceReward(characterClass, currentLevel);
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
            int maxLevel = progression.GetLevelCount(CharacterClass.Player);
            for (int level = 1; level < maxLevel + 1; level++)
            {
                float XPRequired = progression.GetExperienceRequired(CharacterClass.Player, level);
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

        float GetAdditiveModifier()
        {
            throw new NotImplementedException();
        }

        void LevelUpEffect()
        {
            Instantiate(levelUpParticleEffect, transform);
        }
    }
}