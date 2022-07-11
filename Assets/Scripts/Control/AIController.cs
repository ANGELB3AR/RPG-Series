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
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= chaseDistance)
            {
                Debug.Log(name + " should chase");
            }
        }
    }
}