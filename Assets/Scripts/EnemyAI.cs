using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, AI
{
    [SerializeField] private float moveSpeed = 4f; // enemy speed
    [SerializeField] private ObstacleGrid obstacle_data; //obstacles data 
    private Queue<Vector3> pathQueue;//all location on grid
    private float tileSize = 1f;
    private Pathfinding pathfinding; 
    private Vector2Int current_TargetTile; // current moving tile for enemy
    private PlayerMovement playermovement;
    private bool isMoving = false; // to check if its moving or not

    void Start()
    {
        pathQueue = new Queue<Vector3>();
        playermovement = FindObjectOfType<PlayerMovement>();
        StartCoroutine(PlayerStatus());
        pathfinding = FindObjectOfType<Pathfinding>();
    }

    void Update()
    {
        if (pathQueue.Count > 0)
        {
            Vector3 currentTarget = pathQueue.Peek();
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, moveSpeed * Time.deltaTime);
            if (transform.position == currentTarget)
            {
                pathQueue.Dequeue();//reload the path
                if (pathQueue.Count == 0)
                {
                    AlignToCenter(); // make sure the position of enemy is at center of tile
                    isMoving = false;
                }
            }
        }
        pathfinding.LiveEnemyTile(GetEnemyTile()); // sends the enemy tile
    }

    IEnumerator PlayerStatus()
    {
        while (true)
        {
            if (!isMoving && !playermovement.IsMoving)
            {
                PathTrace(playermovement.GetCurrentTile()); //gets player current tile
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void PathTrace(Vector2Int playerTile)
    {
        Vector2Int enemy_Tile = GetEnemyTile();
        List<Vector2Int> adj_Tiles = GetValidAdjacentTiles(playerTile); // gets adjacent tiles
        List<Vector2Int> shortest_path = null;
        foreach (var adj in adj_Tiles)
        {
            var path = Searchpath(enemy_Tile, adj); // use BFS to find shortest path
            if (path.Count > 0 && (shortest_path == null || path.Count < shortest_path.Count))
                shortest_path = path;
        }
        if (shortest_path != null)
        {
            pathQueue.Clear();
            foreach (var step in shortest_path)
            {
                pathQueue.Enqueue(new Vector3(step.x * tileSize, transform.position.y, step.y * tileSize));
            }
            current_TargetTile = shortest_path[^1];
            isMoving = true;
        }
    }

    private List<Vector2Int> GetValidAdjacentTiles(Vector2Int tile)
    {
        List<Vector2Int> valid_tile = new List<Vector2Int>();
        Vector2Int[] directions = {Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right};
        foreach (var dir in directions)
        {
            Vector2Int adj_tiles = tile + dir;
            if (IsValidTile(adj_tiles) && !obstacle_data.CheckObstacle(adj_tiles.x, adj_tiles.y))
                valid_tile.Add(adj_tiles);
        }
        return valid_tile;//return right tile to move
    }

    private List<Vector2Int> Searchpath(Vector2Int start, Vector2Int end) // using BFS since we have only move cost is same
    {
        Queue<Node> open = new Queue<Node>();
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
        open.Enqueue(new Node(start, null));
        visited.Add(start);
        while (open.Count > 0)
        {
            Node current = open.Dequeue();
            if (current.Position == end)
                return Retrace(current); // retrace the path end to start
            foreach (var neighbour in GetValidAdjacentTiles(current.Position))
            {
                if (!visited.Contains(neighbour))
                {
                    visited.Add(neighbour);
                    open.Enqueue(new Node(neighbour, current));
                }
            }
        }
        return new List<Vector2Int>(); 
    }

    private List<Vector2Int> Retrace(Node node)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        while (node != null)
        {
            path.Add(node.Position);
            node = node.Parent;
        }
        path.Reverse();
        return path;
    }

    public Vector2Int GetEnemyTile()
    {
        return new Vector2Int(Mathf.FloorToInt(transform.position.x / tileSize), Mathf.FloorToInt(transform.position.z / tileSize));
    }

    private void AlignToCenter()
    {
        transform.position = new Vector3(Mathf.FloorToInt(transform.position.x / tileSize) * tileSize, transform.position.y, Mathf.FloorToInt(transform.position.z / tileSize) * tileSize);
    }

    private bool IsValidTile(Vector2Int pos) // checking if tile is valid or not
    {
        return pos.x >= 0 && pos.x < 10 && pos.y >= 0 && pos.y < 10;
    }

    private class Node // for setting nodes
    {
        public Vector2Int Position;
        public Node Parent;
        public Node(Vector2Int pos, Node parent)
        {
            Position = pos;
            Parent = parent;
        }
    }
}
