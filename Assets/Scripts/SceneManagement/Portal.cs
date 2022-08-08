using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using RPG.Control;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D, E
        }

        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeInTime = 1f;
        [SerializeField] float fadeWaitTime = 0.5f;

        SavingWrapper savingWrapper;

        void Awake()
        {
            savingWrapper = FindObjectOfType<SavingWrapper>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(Transition());
            }
        }

        IEnumerator Transition()
        {
            if (sceneToLoad < 0)
            {
                Debug.Log("Scene to load not set");
                yield break;
            }

            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();
            PlayerController playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            playerController.enabled = false;

            yield return fader.FadeOut(fadeOutTime);

            savingWrapper.Save();

            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            PlayerController newPlayerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            playerController.enabled = false;

            savingWrapper.Load();

            Portal otherPortal = GetOtherPortal();
            print($"Portal: {otherPortal}");
            UpdatePlayer(otherPortal);

            savingWrapper.Save();

            yield return new WaitForSeconds(fadeWaitTime);
            fader.FadeIn(fadeInTime);

            newPlayerController.enabled = true;
            Destroy(gameObject);
        }

        Portal GetOtherPortal()
        {
            print("Searching for portal");
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) { continue; }
                if (portal.destination != destination) { continue; }
                print($"Found portal: {portal}");
                return portal;
            }
            print("Portal not found");
            return null;
        }

        void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (otherPortal == null)
            {
                Debug.Log($"Unable to find other portal in scene.  Make sure there is a portal with the identifier {destination}");
                return;
            }
            if (player == null)
            {
                Debug.Log("Unable to find object with 'Player' tag.  Please make sure the player is tagged with 'Player'.");
                return;
            }

            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}