using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Combat;

namespace RPG.Attributes
{
    public class HUDDisplay : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI playerHealthText;
        [SerializeField] TextMeshProUGUI enemyHealthText;
        [SerializeField] TextMeshProUGUI xpText;

        Fighter playerFighter;
        Health playerHealth;
        Health enemyHealth;
        Experience playerXP;

        void Awake()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<Health>();
            playerFighter = player.GetComponent<Fighter>();
            playerXP = player.GetComponent<Experience>();
        }

        void Update()
        {
            DisplayPlayerHealth();
            DisplayEnemyHealth();
            DisplayXP();
        }

        void DisplayPlayerHealth()
        {
            playerHealthText.text = $"Health: {playerHealth.GetPercentage().ToString()}%";
        }

        void DisplayEnemyHealth()
        {
            enemyHealth = playerFighter.GetTarget();

            if (enemyHealth == null)
            {
                enemyHealthText.text = "";
            }
            else
            {
                enemyHealthText.text = $"Enemy: {enemyHealth.GetPercentage().ToString()}%";
            }
        }

        void DisplayXP()
        {
            xpText.text = $"XP: {playerXP.GetCurrentExperience().ToString()}";
        }
    }
}