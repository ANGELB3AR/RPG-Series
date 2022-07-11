using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        const float waypointGizmosRadius = 0.25f;

        // Called by Unity
        void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.color = Color.gray;
                Gizmos.DrawSphere(transform.GetChild(i).transform.position, waypointGizmosRadius);
            }
        }
    }
}