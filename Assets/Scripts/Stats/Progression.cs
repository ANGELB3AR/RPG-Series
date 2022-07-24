using UnityEngine;
using System.Collections.Generic;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [NonReorderable]
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;

        Dictionary<CharacterClass, CharacterLevel[]> statLookupTable = null;
 
        void BuildLookup()
        {
            if (statLookupTable != null) { return; }

            statLookupTable = new Dictionary<CharacterClass, CharacterLevel[]>();

            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                CharacterLevel[] levels = progressionClass.levels;
                statLookupTable[progressionClass.characterClass] = levels;
            }
        }

        public float GetHealth(CharacterClass characterClass, int level)
        {
            BuildLookup();
            return statLookupTable[characterClass][level - 1].health;
        }

        public float GetAttackDamage(CharacterClass characterClass, int level)
        {
            BuildLookup();
            return statLookupTable[characterClass][level - 1].attackDamage;
        }

        public float GetExperienceReward(CharacterClass characterClass, int level)
        {
            BuildLookup();
            return statLookupTable[characterClass][level - 1].experienceReward;
        }

        public int GetLevelCount(CharacterClass characterClass)
        {
            BuildLookup();
            return statLookupTable[characterClass].Length;
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public CharacterLevel[] levels;
        }

        [System.Serializable]
        class CharacterLevel
        {
            public float health;
            public float attackDamage;
            public float experienceReward;
            public float experienceToLevelUp;
        }
    }
}