using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyController;

public class ScytheHandler : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private ElementType element;
    [SerializeField] private float attackDuration;
    [SerializeField] private AnimationCurve attackCurve;
    [SerializeField] private Collider damageCollider;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private Transform pivotTransform;

    public float Damage => damage;
    public ElementType Element => element;

    private bool isAttacking;

    public void StartAttack()
    {
        if (isAttacking)
        {
            return;
        }

        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        damageCollider.enabled = true;
        trailRenderer.enabled = true;

        float t = 0;
        Vector3 startRotation = Vector3.zero;
        Vector3 endRotation = Vector3.up * 355;

        while (t < 1)
        {
            t += Time.deltaTime / attackDuration;
            pivotTransform.localEulerAngles = Vector3.Lerp(startRotation, endRotation, attackCurve.Evaluate(t));

            yield return null;
        }

        pivotTransform.localEulerAngles = startRotation;
        damageCollider.enabled = false;
        trailRenderer.enabled = false;
        isAttacking = false;
    }
}
