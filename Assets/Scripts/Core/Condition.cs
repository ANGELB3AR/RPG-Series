using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    [System.Serializable]
    public class Condition
    {
        [NonReorderable]
        [SerializeField] Disjunction[] and;

        public bool Check(IEnumerable<IPredicateEvaluator> evaluators)
        {
            foreach (Disjunction disjunction in and)
            {
                if (!disjunction.Check(evaluators)) { return false; }
            }
            return true;
        }

        [System.Serializable]
        class Disjunction
        {
            [NonReorderable]
            [SerializeField] Predicate[] or;

            public bool Check(IEnumerable<IPredicateEvaluator> evaluators)
            {
                foreach (Predicate predicate in or)
                {
                    if (predicate.Check(evaluators)) { return true; }
                }
                return false;
            }
        }

        [System.Serializable]
        class Predicate
        {
            [SerializeField] string predicate;
            [NonReorderable]
            [SerializeField] string[] parameters;
            [SerializeField] bool negate = false;

            public bool Check(IEnumerable<IPredicateEvaluator> evaluators)
            {
                foreach (var evaluator in evaluators)
                {
                    bool? result = evaluator.Evaluate(predicate, parameters);

                    if (result == null) { continue; }
                    if (result == negate) { return false; }
                }
                return true;
            }
        }
    }
}