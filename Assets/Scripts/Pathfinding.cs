using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] private ObstacleGrid obstacleGrid; // Obstacle grid 
    private EnemySpawn enemy_tile; //enemy spawn position
    private Vector2Int startTile; // current tile
    private Vector2Int endTile; // target tile

    private void Awake()
    {
        enemy_tile = FindObjectOfType<EnemySpawn>();
    }

    public List<Vector2Int> FindPath(Vector2Int start, Vector2Int end)
    {
        startTile = start;
        endTile = end;
        List<Node> open_list = new List<Node>(); // openlist for A* path finding
        List<Node> closed_list = new List<Node>(); // close list
        Node start_node = new Node(start, 0, GetHeuristic(start, end), null); 
        open_list.Add(start_node);
        while (open_list.Count > 0)
        {
            Node current_node = GetLowestFNode(open_list); // Get node with the lowest F score
            open_list.Remove(current_node);
            closed_list.Add(current_node);
            if (current_node.Position == end)
            {
                // Path found
                return Retrace(current_node);
            }
            foreach (Vector2Int neighbor in GetNeighbors(current_node.Position))
            {
                if (IsValidTile(neighbor) && !IsObstacle(neighbor) && !IsEnemyPosition(neighbor) && !closed_list.Exists(n => n.Position == neighbor))
                {
                    Node neighbor_node = new Node(neighbor, current_node.GCost + 1, GetHeuristic(neighbor, end), current_node);
                    open_list.Add(neighbor_node);
                }
            }
        }
        return new List<Vector2Int>(); 
    }

    private Node GetLowestFNode(List<Node> nodes)
    {
        Node lowest_Fnode = nodes[0];
        foreach (Node node in nodes)
        {
            if (node.FCost < lowest_Fnode.FCost)
            {
                lowest_Fnode = node;
            }
        }
        return lowest_Fnode;
    }

    private List<Vector2Int> Retrace(Node endNode)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Node current_node = endNode;

        while (current_node != null)
        {
            path.Add(current_node.Position);
            current_node = current_node.Parent;
        }
        path.Reverse(); // Reversing the path
        return path;
    }

    private List<Vector2Int> GetNeighbors(Vector2Int position)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();
        Vector2Int[] directions = {Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right};

        foreach (Vector2Int dir in directions)
        {
            Vector2Int neighborPos = position + dir;
            if (neighborPos.x >= 0 && neighborPos.x < 10 && neighborPos.y >= 0 && neighborPos.y < 10)
            {
                neighbors.Add(neighborPos);
            }
        }
        return neighbors;
    }

    private bool IsValidTile(Vector2Int tilePosition)
    {
        return tilePosition.x >= 0 && tilePosition.x < 10 && tilePosition.y >= 0 && tilePosition.y < 10; // checking for bounds
    }

    private bool IsObstacle(Vector2Int tilePosition)
    {
        return obstacleGrid.CheckObstacle(tilePosition.x, tilePosition.y); // checking for obstacle
    }

    private bool IsEnemyPosition(Vector2Int tilePosition)
    {
        return enemy_tile.GetEnemyPosition() == tilePosition; // checking for enemy tile
    }

    private int GetHeuristic(Vector2Int position, Vector2Int end)
    {
        return Mathf.Abs(position.x - end.x) + Mathf.Abs(position.y - end.y);
    }

    private class Node
    {
        public Vector2Int Position;
        public int GCost;
        public int HCost;
        public Node Parent;
        public Node(Vector2Int position, int gCost, int hCost, Node parent)
        {
            Position = position;
            GCost = gCost;
            HCost = hCost;
            Parent = parent;
        }
        public int FCost => GCost + HCost; 
    }
}