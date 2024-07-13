using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            Vector3 position = new Vector3(x, Y, z);

            var data1 = Physics.OverlapSphere(position, 0.5f);
            if (data1.Count(x => x.gameObject.layer == (int)Layers.Tower || 
                x.gameObject.layer == (int)Layers.Mines || x.gameObject.layer == (int)Layers.Obstacle) > 0)
            {
                i--;
                continue;
            }

            Instantiate(obstacle, new Vector3(x, Y, z), Quaternion.identity);
        }
    }
}
