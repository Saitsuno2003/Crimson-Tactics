using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private ObstacleGrid obstacle_data; // Obstacle data
    [SerializeField] private GameObject player_prefab; // Player prefab
    [SerializeField] private float tile_space = 1f; // tile space for the player  
    private EnemySpawn enemy_tile; // enemy tile position

    private void Awake()
    {
        enemy_tile = FindObjectOfType<EnemySpawn>();
    }

    void Start()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        Vector2Int enemyPos = enemy_tile.GetEnemyPosition(); // gets the enemy spawn tile
        List<Vector2Int> spawn_tiles = new List<Vector2Int>(); // gets all tiles 
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Vector2Int current_tile = new Vector2Int(i, j);
                if (!obstacle_data.CheckObstacle(i, j) && current_tile != enemyPos) // checking for the enemy spawn tile and obstacle
                    spawn_tiles.Add(new Vector2Int(i, j));
            }
        }
        Vector2Int spawn_Pos = spawn_tiles[Random.Range(0, spawn_tiles.Count)]; // player spawn tile
        Vector3 player_Pos = new Vector3(spawn_Pos.x * tile_space, 1.5f, spawn_Pos.y * tile_space); // player position
        GameObject player = Instantiate(player_prefab, player_Pos, Quaternion.identity);
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        MouseHover mouseHover = FindObjectOfType<MouseHover>();
        if (mouseHover != null && movement != null)
        {
            mouseHover.SetPlayerMovement(movement); 
        }
    }
}