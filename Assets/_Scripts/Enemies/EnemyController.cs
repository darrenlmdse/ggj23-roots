using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private float health = 5;
    [SerializeField] private ElementType element;

    public enum ElementType
    {
        None, Water, Grass, Fire
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        Transform closestRoot = PlantManager.Instance.GetClosestRoot(transform.position);

        if (closestRoot != null)
        {
            agent.SetDestination(closestRoot.position);
        }

        // TODO: need something to happen here if no roots currently exist
    }

    private void OnTriggerEnter(Collider other)
    {
        ScytheHandler scythe = other.GetComponent<ScytheHandler>();

        if (scythe == null)
        {
            return;
        }

        TakeDamage(scythe);
    }

    private void TakeDamage(ScytheHandler scythe)
    {
        switch (element)
        {
            case ElementType.Water:
                switch (scythe.Element)
                {
                    case ElementType.None:
                        health -= scythe.Damage;
                        break;

                    case ElementType.Water:
                        health -= scythe.Damage / 2;
                        break;

                    case ElementType.Grass:
                        health -= scythe.Damage * 2;
                        break;

                    case ElementType.Fire:
                        health -= 0;
                        break;
                }
                break;

            case ElementType.Grass:
                switch (scythe.Element)
                {
                    case ElementType.None:
                        health -= scythe.Damage;
                        break;

                    case ElementType.Water:
                        health -= 0;
                        break;

                    case ElementType.Grass:
                        health -= scythe.Damage / 2;
                        break;

                    case ElementType.Fire:
                        health -= scythe.Damage * 2;
                        break;
                }
                break;

            case ElementType.Fire:
                switch (scythe.Element)
                {
                    case ElementType.None:
                        health -= scythe.Damage;
                        break;

                    case ElementType.Water:
                        health -= scythe.Damage * 2;
                        break;

                    case ElementType.Grass:
                        health -= 0;
                        break;

                    case ElementType.Fire:
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
    }
}
