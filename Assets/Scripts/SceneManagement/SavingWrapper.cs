using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Saving;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        [SerializeField] float fadeInTime = 0.2f;

        SavingSystem savingSystem;
        Fader fader;
        const string defaultSaveFile = "save";

        void Awake()
        {
            savingSystem = GetComponent<SavingSystem>();
            fader = FindObjectOfType<Fader>();

            StartCoroutine(LoadLastScene());
        }

        IEnumerator LoadLastScene()
        {
            yield return savingSystem.LoadLastScene(defaultSaveFile);
            fader.FadeOutImmediate();
            yield return fader.FadeIn(fadeInTime);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Delete();
            }

        }

        public void Load()
        {
            StartCoroutine(savingSystem.LoadLastScene(defaultSaveFile));
        }

        public void Save()
        {
            savingSystem.Save(defaultSaveFile);
        }

        public void Delete()
        {
            savingSystem.Delete(defaultSaveFile);
        }
    }
}