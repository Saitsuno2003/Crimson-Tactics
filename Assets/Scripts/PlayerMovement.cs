using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // movement speed
    private Queue<Vector3> path_Queue; // follow queue
    private Pathfinding pathfinding; // A* Pathfinding
    private float tileSize = 1f; 

    void Start()
    {
        pathfinding = FindObjectOfType<Pathfinding>();
        path_Queue = new Queue<Vector3>();
    }

    void Update()
    {
        if (path_Queue.Count > 0)
        {
            Vector3 current_Target = path_Queue.Peek(); // next position of the queue
            transform.position = Vector3.MoveTowards(transform.position, current_Target, moveSpeed * Time.deltaTime);

            if (transform.position == current_Target)
            {
                path_Queue.Dequeue(); // target removed
                if (path_Queue.Count == 0)
                {
                    AlignToTileCenter(); // Align the player to the center of the last target tile
                }
            }
        }
    }

    public void MoveToTile(Vector2Int targetTile)
    {
        Vector2Int current_Tile = GetCurrentTile(); // current tile position
        List<Vector2Int> path = pathfinding.FindPath(current_Tile, targetTile); // pathfinding
        if (path.Count > 0)
        {
            path_Queue.Clear(); // clear path
            foreach (Vector2Int tile in path)
            {
                Vector3 targetPos = new Vector3(tile.x * tileSize, transform.position.y, tile.y * tileSize); // postion to world posiion
                path_Queue.Enqueue(targetPos); 
            }
        }
    }

    public bool IsMoving => path_Queue.Count > 0; 

    private Vector2Int GetCurrentTile()
    {
        int tileX = Mathf.FloorToInt(transform.position.x / tileSize);
        int tileZ = Mathf.FloorToInt(transform.position.z / tileSize);
        return new Vector2Int(tileX, tileZ);
    }

    private void AlignToTileCenter()
    {
        Vector3 alignedPosition = new Vector3(Mathf.FloorToInt(transform.position.x / tileSize) * tileSize ,transform.position.y,Mathf.FloorToInt(transform.position.z / tileSize) * tileSize);
        transform.position = alignedPosition;
    }
}
