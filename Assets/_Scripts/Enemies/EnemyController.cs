using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private float health = 5;
    [SerializeField] private ElementalType element;
    [SerializeField] private Transform spriteTransform;
    [SerializeField] private float damage;
    [SerializeField] private float damageInterval;
    [SerializeField] private AnimationCurve damageIndicatorCurve;
    [SerializeField] private float damageIndicatorInterval;
    [SerializeField] private Sprite[] sprites;

    private Quaternion baseSpriteRotation;
    private RootHandler targetRoot;

    public ElementalType Element
    {
        set
        {
            element = value;

            SpriteRenderer sprite = spriteTransform.GetComponent<SpriteRenderer>();
            sprite.sprite = sprites[(int)element];
        }
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        baseSpriteRotation = spriteTransform.rotation;
    }

    private void Start()
    {
        FindRoot();
    }

    private void FindRoot()
    {
        Transform closestRoot = PlantManager.Instance.GetClosestRoot(transform.position);

        if (closestRoot != null)
        {
            agent.SetDestination(closestRoot.position);
        }
        else
        {
            StartCoroutine(WaitForNewRoot());
        }
    }

    private IEnumerator WaitForNewRoot()
    {
        yield return new WaitForSeconds(damageInterval);

        FindRoot();
    }

    private void Update()
    {
        spriteTransform.rotation = baseSpriteRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        ScytheHandler scythe = other.GetComponent<ScytheHandler>();

        if (scythe != null)
        {
            TakeDamage(scythe);
            return;
        }

        RootHandler root = other.GetComponent<RootHandler>();

        if (root != null)
        {
            targetRoot = root;
            StartCoroutine(EatRoot());
            return;
        }
    }

    private void TakeDamage(ScytheHandler scythe)
    {
        float startingHealth = health;

        switch (element)
        {
            case ElementalType.Water:
                switch (scythe.Element)
                {
                    case ElementalType.Neutral:
                        health -= scythe.Damage;
                        break;

                    case ElementalType.Water:
                        health -= scythe.Damage / 2;
                        break;

                    case ElementalType.Leaf:
                        health -= scythe.Damage * 2;
                        break;

                    case ElementalType.Fire:
                        health -= 0;
                        break;
                }
                break;

            case ElementalType.Leaf:
                switch (scythe.Element)
                {
                    case ElementalType.Neutral:
                        health -= scythe.Damage;
                        break;

                    case ElementalType.Water:
                        health -= 0;
                        break;

                    case ElementalType.Leaf:
                        health -= scythe.Damage / 2;
                        break;

                    case ElementalType.Fire:
                        health -= scythe.Damage * 2;
                        break;
                }
                break;

            case ElementalType.Fire:
                switch (scythe.Element)
                {
                    case ElementalType.Neutral:
                        health -= scythe.Damage;
                        break;

                    case ElementalType.Water:
                        health -= scythe.Damage * 2;
                        break;

                    case ElementalType.Leaf:
                        health -= 0;
                        break;

                    case ElementalType.Fire:
                        health -= scythe.Damage / 2;
                        break;
                }
                break;
        }

        if (health <= 0)
        {
            // TODO: Leave a corpse behind here
            Destroy(gameObject);
        }

        if (startingHealth != health)
        {

            StopCoroutine(IndicateDamageTaken());
            StartCoroutine(IndicateDamageTaken());
        }
    }

    private IEnumerator IndicateDamageTaken()
    {
        float t = 0;

        SpriteRenderer sprite = spriteTransform.GetComponent<SpriteRenderer>();
        Color colour = sprite.color;
         
        while (t < 1)
        {
            t += Time.deltaTime / damageIndicatorInterval;

            float intentsity = damageIndicatorCurve.Evaluate(t);

            colour.r = intentsity;
            colour.g = intentsity;
            colour.b = intentsity;

            sprite.color = colour;

            yield return null;
        }
    }

    private IEnumerator EatRoot()
    {
        yield return new WaitForSeconds(damageInterval);

        if (targetRoot != null)
        {
            targetRoot.TakeDamage(damage);
            StartCoroutine(EatRoot());
        }
        else
        {
            FindRoot();
        }
    }
}
