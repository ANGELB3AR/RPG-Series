using UnityEngine;
using System.Collections.Generic;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [NonReorderable]
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;

        //Dictionary<CharacterClass, Dictionary<int[], float>> lookupTable = null;

        //void BuildLookup()
        //{
        //    if (lookupTable != null) { return; }

        //    lookupTable = new Dictionary<CharacterClass, Dictionary<int[], float>>();

        //    foreach (ProgressionCharacterClass progressionClass in characterClasses)
        //    {
        //        var levelLookupTable = new Dictionary<int[], float>();

        //        foreach (CharacterLevel level in ProgressionCharacterClass.levels)
        //        {
        //            levelLookupTable[level] = CharacterLevel.health;
        //        }

        //        lookupTable[progressionClass.characterClass] = levelLookupTable;
        //    }
        //}

        //public float GetHealth(CharacterClass characterClass, int level)
        //{
        //    foreach (ProgressionCharacterClass progressionClass in characterClasses)
        //    {
        //        if (progressionClass.characterClass == characterClass)
        //        {
        //            return progressionClass.levels[level - 1].health;
        //        }
        //    }
        //    return 0;
        //}

        //public float GetExperienceReward(CharacterClass characterClass, int level)
        //{
        //    foreach (ProgressionCharacterClass progressionClass in characterClasses)
        //    {
        //        if (progressionClass.characterClass == characterClass)
        //        {
        //            return progressionClass.levels[level - 1].experienceReward;
        //        }
        //    }
        //    return 0;
        //}

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
        }
    }
}