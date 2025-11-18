using UnityEngine;

public class EnemyVFX : EntityVFX
{
    [Header("Counter Window")]
    [SerializeField] private GameObject counterAlert;   // Temporarily deprecated

    public void EnableCounterAlert(bool enable)
    {
        if (!counterAlert)
            return;

        counterAlert.SetActive(enable);
    }
}
