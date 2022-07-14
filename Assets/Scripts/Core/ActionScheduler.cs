using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;

        public void StartAction(IAction action)
        {
            currentAction = action;
           
            if (currentAction == action) { return; }
            if (currentAction != null)
            {
                currentAction.Cancel();
            }
        }

        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }
}