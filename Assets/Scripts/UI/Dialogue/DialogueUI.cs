using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Dialogue;
using TMPro;

namespace RPG.UI
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI AIText;
        PlayerConversant playerConversant;

        void Awake()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
        }

        void Start()
        {
            AIText.text = playerConversant.GetText();
        }
    }
}