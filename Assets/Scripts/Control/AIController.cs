using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        void Update()
        {
            if (DistanceToPlayer() <= chaseDistance)
            {
                Debug.Log(name + " should chase");
            }
        }

        float DistanceToPlayer()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            return Vector3.Distance(transform.position, player.transform.position);
        }
    }
}