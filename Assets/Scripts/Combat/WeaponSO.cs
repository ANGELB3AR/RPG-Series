using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/Create New Weapon")]
    public class WeaponSO : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamage = 5f;

        public void Spawn(Transform handTransform, Animator animator)
        {
            if (equippedPrefab != null)
            {
                Instantiate(equippedPrefab, handTransform);
            }
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }

        public float GetRange()
        {
            return weaponRange;
        }

        public float GetDamage()
        {
            return weaponDamage;
        }
    }
}