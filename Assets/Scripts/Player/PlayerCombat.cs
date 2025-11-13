using UnityEngine;

public class PlayerCombat : EntityCombat
{
    [Header("Counter attack details")]
    [SerializeField] private float counterRecovery = .1f;

    public bool CounterAttackPerformed()
    {
        bool hasPerformedCounter = false;

        foreach (var target in GetDetectedCollider())
        {
            ICounterable counterable = target.GetComponent<ICounterable>();

            if (counterable == null)
            {
                continue;
            }

            if (counterable.CanBeCountered)
            {
                counterable.HandleCounter();
                hasPerformedCounter = true;
            }
        }

        return hasPerformedCounter;
    }

    public float GetCounterRecoveryDuration() => counterRecovery;
}
