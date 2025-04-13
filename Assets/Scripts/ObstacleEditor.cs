using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObstacleGrid))]
public class ObstacleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ObstacleGrid obstacle_data = (ObstacleGrid)target; // gets the grid data
        GUILayout.Label("Obstacle Grid", EditorStyles.boldLabel);
        for (int j = 0; j < 10; j++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int i = 0; i < 10; i++)
            {
                bool current = obstacle_data.CheckObstacle(i, j);
                bool updated = EditorGUILayout.Toggle(current, GUILayout.Width(25)); // editor layout
                if (current != updated)
                {
                    obstacle_data.SetObstacle(i, j, updated); // make the obstacle data updated
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        if (GUILayout.Button("Save"))
        {
            EditorUtility.SetDirty(obstacle_data); // save the data
            AssetDatabase.SaveAssets();
        }
    }
}
