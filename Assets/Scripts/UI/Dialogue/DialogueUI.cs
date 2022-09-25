using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Dialogue;
using TMPro;
using UnityEngine.UI;

namespace RPG.UI
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI AIText;
        [SerializeField] Button nextButton;

        PlayerConversant playerConversant;

        void Awake()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
        }

        void Start()
        {
            UpdateUI();
            nextButton.onClick.AddListener(Next);
        }

        void UpdateUI()
        {
            AIText.text = playerConversant.GetText();
            nextButton.gameObject.SetActive(playerConversant.HasNext());
        }

        void Next()
        {
            playerConversant.Next();
            UpdateUI();
        }
    }
}