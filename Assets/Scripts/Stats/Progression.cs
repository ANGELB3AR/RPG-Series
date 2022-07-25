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
            return statLookupTable[characterClass][level].GetStatValue(stat);
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
            public float health;
            public float attackDamage;
            public float experienceReward;
            public float experienceRequired;

            public float GetStatValue(Stat stat)
            {
                switch (stat)
                {
                    case Stat.Health:
                        return health;
                    case Stat.AttackDamage:
                        return attackDamage;
                    case Stat.ExperienceReward:
                        return experienceReward;
                    case Stat.ExperienceRequired:
                        return experienceRequired;
                    default:
                        return -1;
                }
            }
        }
    }
}
/*  TODO:
 *      - FIGURE OUT HOW TO CONVERT FLOAT STATS TO STAT STATS & CONSOLIDATE STAT LOOKUPS TO A SINGLE METHOD
 *      - IMPLEMENT IMODIFIER AGAIN USING STAT STATS
 */
