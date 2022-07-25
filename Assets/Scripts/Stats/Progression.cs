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

        public float GetStat(CharacterClass characterClass, int level, StatValue.Stat stat)
        {
            BuildLookup();
            return statLookupTable[characterClass][level].GetStatValue(stat);
        }

        //public float GetHealth(CharacterClass characterClass, int level)
        //{
        //    BuildLookup();
        //    return statLookupTable[characterClass][level - 1].health;
        //}

        //public float GetAttackDamage(CharacterClass characterClass, int level)
        //{
        //    BuildLookup();
        //    return statLookupTable[characterClass][level - 1].attackDamage;
        //}

        //public float GetExperienceReward(CharacterClass characterClass, int level)
        //{
        //    BuildLookup();
        //    return statLookupTable[characterClass][level - 1].experienceReward;
        //}

        //public float GetExperienceRequired(CharacterClass characterClass, int level)
        //{
        //    BuildLookup();
        //    return statLookupTable[characterClass][level - 1].experienceRequired;
        //}


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
            public StatValue health;
            public StatValue attackDamage;
            public StatValue experienceReward;
            public StatValue experienceRequired;

            public float GetStatValue(StatValue.Stat stat)
            {
                if (stat == StatValue.Stat.Health)
                {
                    return (float)StatValue.Stat.Health;
                }
                else if (stat == StatValue.Stat.AttackDamage)
                {
                    return (float)StatValue.Stat.AttackDamage;
                }
                else if (stat == StatValue.Stat.ExperienceReward)
                {
                    return (float)StatValue.Stat.ExperienceReward;
                }
                else if (stat == StatValue.Stat.ExperienceRequired)
                {
                    return (float)StatValue.Stat.ExperienceRequired;
                }
                else return 0;
            }
        }

        [System.Serializable]
        public class StatValue
        {
            public enum Stat
            {
                Health,
                AttackDamage,
                ExperienceReward,
                ExperienceRequired
            }
        }
    }
}
/*  TODO:
 *      - FIGURE OUT HOW TO CONVERT FLOAT STATS TO STAT STATS & CONSOLIDATE STAT LOOKUPS TO A SINGLE METHOD
 *      - IMPLEMENT IMODIFIER AGAIN USING STAT STATS
 */
