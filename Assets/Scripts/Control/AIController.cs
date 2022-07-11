using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        Animator animator;
        string attackAnimParam = "attack";

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (DistanceToPlayer() <= chaseDistance)
            {
                animator.SetTrigger(attackAnimParam);
            }
            else
            {
                animator.ResetTrigger(attackAnimParam);
            }
        }

        float DistanceToPlayer()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            return Vector3.Distance(transform.position, player.transform.position);
        }
    }
}