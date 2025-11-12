using UnityEngine;
using System.Collections;

public class EnemyDeadState : EnemyState
{
    private SpriteRenderer sr;
    private float fadeDuration;
    private float deathAnimDuration;

    public EnemyDeadState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        sr = enemy.GetComponentInChildren<SpriteRenderer>();
        fadeDuration = enemy.fadeDuration;
        deathAnimDuration = enemy.deathAnimDuration;
    }

    public override void Enter()
    {
        base.Enter();

        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;
        enemy.StartCoroutine(DeathSequence());
        stateMachine.SwitchOffStateMachine();
    }

    private IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(deathAnimDuration);

        anim.enabled = false;
        float elapsed = 0f;
        Color originalColor = sr.color;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        Object.Destroy(enemy.gameObject);
    }
}
