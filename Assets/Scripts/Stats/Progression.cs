using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [NonReorderable]
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;

        [System.Serializable]
        class ProgressionCharacterClass
        {
            [SerializeField] CharacterClass characterClass;
            [SerializeField] CharacterLevel[] levels;
        }

        [System.Serializable]
        class CharacterLevel
        {
            [SerializeField] float health;
            [SerializeField] float attackDamage;
        }
    }
}