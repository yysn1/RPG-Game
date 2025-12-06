using System.Collections;
using UnityEngine;

[System.Serializable]
public struct Buff
{
    public StatType type;
    public float value;
}

public class Object_Buff : MonoBehaviour
{
    private SpriteRenderer sr;
    private EntityStats statsToModify;

    [Header("Buff Settings")]
    [SerializeField] private Buff[] buffs;
    [SerializeField] private string buffName;
    [SerializeField] private bool canBeUsed = true;
    [SerializeField] private float buffDuration = 4f;

    [Header("Floating Settings")]
    [SerializeField] private float floatSpeed = 1f;
    [SerializeField] private float floatRange = .1f;
    private Vector3 startPosition;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        startPosition = transform.position;
    }

    private void Update()
    {
        UpdateFloatingPosition();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canBeUsed)
        {
            return;
        }

        statsToModify = collision.GetComponent<EntityStats>();

        StartCoroutine(BuffCo(buffDuration));
    }

    private IEnumerator BuffCo(float duration)
    {
        canBeUsed = false;
        sr.color = Color.clear;

        ApplyBuff(true);
        
        yield return new WaitForSeconds(duration);

        ApplyBuff(false);
        Destroy(gameObject);
    }

    private void ApplyBuff(bool apply)
    {
        foreach (var buff in buffs)
        {
            if (apply)
            {
                statsToModify.GetStatByType(buff.type).AddModifier(buff.value, buffName);
            }
            else
            {
                statsToModify.GetStatByType(buff.type).RemoveModifier(buffName);
            }
        }
    }

    private void UpdateFloatingPosition()
    {
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatRange;
        transform.position = startPosition + new Vector3(0, yOffset);
    }
}
