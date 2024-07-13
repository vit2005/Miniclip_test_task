using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject obstacle;
    [SerializeField] int count;
    [SerializeField] int Y, maxX, minX, maxZ, minZ;

    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            var x = Random.Range(minX, maxX);
            var z = Random.Range(minZ, maxZ);

            Instantiate(obstacle, new Vector3(x, Y, z), Quaternion.identity);
        }
    }
}
