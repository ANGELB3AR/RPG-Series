using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,99)]
        [SerializeField] int currentLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;

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
            GetComponent<Experience>();
        }
    }
}