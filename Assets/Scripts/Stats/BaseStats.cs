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
                //print($"Player level: {GetCurrentLevel()}");
                print($"Player Level Count: {progression.GetLevelCount(CharacterClass.Player)}");
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

        //public int GetCurrentLevel()
        //{
        //    float currentXP = GetComponent<Experience>().GetCurrentExperience();

        //}
    }
}