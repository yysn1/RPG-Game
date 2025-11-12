using UnityEngine;

public class EnemyVFX : EntityVFX
{
    [Header("Counter Window")]
    [SerializeField] private GameObject counterAlert;

    public void EnableCounterAlertWindow(bool enable) => counterAlert.SetActive(enable);
}
