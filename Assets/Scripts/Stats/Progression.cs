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

        public float GetStat(CharacterClass characterClass, int level, Stat stat)
        {
            BuildLookup();
            return statLookupTable[characterClass][level].stat;
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

        public float GetExperienceRequired(CharacterClass characterClass, int level)
        {
            BuildLookup();
            return statLookupTable[characterClass][level - 1].experienceRequired;
        }


        public int GetLevelCount(CharacterClass characterClass)
        {
            BuildLookup();
            return statLookupTable[characterClass].Length;
        }

        [System.Serializable]
        public class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public CharacterLevel[] levels;
        }

        [System.Serializable]
        public class CharacterLevel
        {
            public Stat health;
            public Stat attackDamage;
            public Stat experienceReward;
            public Stat experienceRequired;

            [System.Serializable]
            public class Stat
            {
                float baseValue;

                public float GetValue()
                {
                    return baseValue;
                }
            }
        }
}
/*  TODO:
 *      - FIGURE OUT HOW TO CONVERT FLOAT STATS TO STAT STATS & CONSOLIDATE STAT LOOKUPS TO A SINGLE METHOD
 *      - IMPLEMENT IMODIFIER AGAIN USING STAT STATS
 */
