using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Combat;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI playerHealthText;
        [SerializeField] TextMeshProUGUI enemyHealthText;

        Fighter playerFighter;
        Health playerHealth;
        Health enemyHealth;

        void Awake()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<Health>();
            playerFighter = player.GetComponent<Fighter>();
        }

        void Update()
        {
            DisplayPlayerHealth();
            DisplayEnemyHealth();
        }

        private void DisplayPlayerHealth()
        {
            playerHealthText.text = $"Health: {playerHealth.GetPercentage().ToString()}%";
        }

        private void DisplayEnemyHealth()
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
    }
}