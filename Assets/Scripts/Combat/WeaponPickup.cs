using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;
        [SerializeField] float respawnTime = 5f;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                StartCoroutine(HideForSeconds(respawnTime));
            }
        }

        IEnumerator HideForSeconds(float seconds)
        {
            HidePickup();
            yield return new WaitForSeconds(seconds);
            ShowPickup();
        }

        void HidePickup()
        {
            throw new NotImplementedException();
        }

        void ShowPickup()
        {
            throw new NotImplementedException();
        }
    }
}