using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRandomizer : MonoBehaviour
{
    private void Awake()
    {
        for (int j = 0; j < transform.childCount; j++)
        {
            for (int i = 0; i < transform.GetChild(j).childCount; i++)
            {
                Vector3 scale = transform.GetChild(j).GetChild(i).localScale;

                int rng = Random.Range(0, 2);

                if (rng > 0)
                {
                    scale.x *= -1;
                }

                rng = Random.Range(0, 2);

                if (rng > 0)
                {
                    scale.y *= -1;
                }

                rng = Random.Range(0, 4);

                transform.GetChild(j).GetChild(i).localEulerAngles = new Vector3(-90f, rng * 90f, 0f);
            }
        }
    }
}
