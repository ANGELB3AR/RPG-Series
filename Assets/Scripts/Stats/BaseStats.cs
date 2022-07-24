using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,99)]
        [SerializeField] int currentLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;

        private void Start()
        {
            currentLevel = CalculateCurrentLevel();
        }

        void Update()
        {
            int newLevel = CalculateCurrentLevel();
            if (newLevel > currentLevel)
            {
                currentLevel = newLevel;
                print("Leveled up!");
            }
        }

        public float GetHealth()
        {
            return progression.GetHealth(characterClass, currentLevel);
        }

        public float GetExperienceReward()
        {
            return progression.GetExperienceReward(characterClass, currentLevel);
        }

        public int GetCurrentLevel()
        {
            return currentLevel;
        }

        public int CalculateCurrentLevel()
        {
            float currentXP = GetComponent<Experience>().GetCurrentExperience();
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
    }
}