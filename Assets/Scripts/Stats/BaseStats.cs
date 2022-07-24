using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,99)]
        [SerializeField] int currentLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;

        private void Update()
        {
            if (gameObject.CompareTag("Player"))
            {
                print($"Player level: {GetCurrentLevel()}");
            }
        }

        public float GetHealth()
        {
            return progression.GetHealth(characterClass, currentLevel);
        }

        public float GetExperienceReward()
        {
            return progression.GetExperience(characterClass, currentLevel);
        }

        public int GetCurrentLevel()
        {
            float currentXP = GetComponent<Experience>().GetCurrentExperience();
            for (int i = 0; i < progression.GetLevelCount(CharacterClass.Player); i++)
            {
                if (progression.GetExperience(CharacterClass.Player, i) > currentXP)
                {
                    return i;
                }
            }
            return progression.GetLevelCount(CharacterClass.Player) + 1;
        }
    }
}