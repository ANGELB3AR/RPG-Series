using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Combat;
using RPG.Stats;

namespace RPG.Attributes
{
    public class HUDDisplay : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI playerHealthText;
        [SerializeField] TextMeshProUGUI enemyHealthText;
        [SerializeField] TextMeshProUGUI xpText;
        [SerializeField] TextMeshProUGUI currentLevelText;

        Fighter playerFighter;
        Health playerHealth;
        Health enemyHealth;
        Experience playerXP;
        BaseStats baseStats;

        void Awake()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<Health>();
            playerFighter = player.GetComponent<Fighter>();
            playerXP = player.GetComponent<Experience>();
            baseStats = player.GetComponent<BaseStats>();
        }

        void Update()
        {
            DisplayPlayerHealth();
            DisplayEnemyHealth();
            DisplayXP();
            DisplayCurrentLevel();
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

        void DisplayCurrentLevel()
        {
            currentLevelText.text = $"Level: {baseStats.GetCurrentLevel().ToString()}";
        }
    }
}