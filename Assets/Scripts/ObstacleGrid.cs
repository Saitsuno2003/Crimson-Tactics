using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleGrid", menuName = "Grid/ObstacleGrid")]
public class ObstacleGrid : ScriptableObject
{
    [SerializeField] private List<bool> grid = new List<bool>(100);

    public void SetObstacle(int x, int y, bool isObstacle)
    {
        if (grid == null || grid.Count != 100) // make sure the grid count is under the range
            grid = new List<bool>(new bool[100]);

        grid[y * 10 + x] = isObstacle;
    }

    public bool CheckObstacle(int x, int y)
    {
        if (grid == null || grid.Count != 100)  
            grid = new List<bool>(new bool[100]);
        
        return grid[y * 10 + x]; // returns if the grid have obstacle or not
    }
}
