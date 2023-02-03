using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetupManager : MonoBehaviour
{
    public static GameSetupManager Instance;

    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform surfaceEnvironment;
    [SerializeField] private Transform undergroundEnvironment;

    public Vector3 Offset => offset;

    private void Awake()
    {
        undergroundEnvironment.position = surfaceEnvironment.position + offset;
    }
}
