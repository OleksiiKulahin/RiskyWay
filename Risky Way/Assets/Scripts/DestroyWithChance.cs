using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithChance : MonoBehaviour
{
    [Range(0, 1)]
    public float chanceOfStaying = 0.8f;
    void Start()
    {
        if (Random.value > chanceOfStaying) { Destroy(gameObject); }
    }
}
