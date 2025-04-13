using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private ObstacleGrid obstacle_data; // obstacle scriptableobject data
    [SerializeField] private GameObject redSphere_prefab; // obstacle game object

    void Start()
    {
        GenerateObstacles(); // generate the obstacle
    }

    void GenerateObstacles()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (obstacle_data.CheckObstacle(i, j)) // checks if the grid is for obstacle
                {
                    Vector3 pos = new Vector3(i, 1f, j);
                    Instantiate(redSphere_prefab, pos, Quaternion.identity);
                }
            }
        }
    }
}
