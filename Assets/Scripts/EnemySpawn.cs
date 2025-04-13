using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private ObstacleGrid obstacle_data; // Obstacle data
    [SerializeField] private GameObject enemy_prefab; // Enemy prefab
    [SerializeField] private float tile_space = 1f; // tile space for the player  
    private Vector2Int enemy_tile_position;

    void Start()
    {
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        List<Vector2Int> spawn_tiles = new List<Vector2Int>();
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (!obstacle_data.CheckObstacle(i, j)) // check if there is an obstacle on the grid or not 
                    spawn_tiles.Add(new Vector2Int(i, j));
            }
        }
        Vector2Int spawn_Pos = spawn_tiles[Random.Range(0, spawn_tiles.Count)];
        Vector3 enemy_Pos = new Vector3(spawn_Pos.x * tile_space, 1.5f, spawn_Pos.y * tile_space);
        enemy_tile_position = spawn_Pos;
        Instantiate(enemy_prefab, enemy_Pos, Quaternion.identity);
    }

    public Vector2Int GetEnemyPosition() => enemy_tile_position; 
}