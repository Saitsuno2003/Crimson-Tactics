using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    [SerializeField] private GameObject tile; //tile prefab 
    [SerializeField] private int height = 10, width = 10; // grid total size
    [SerializeField] private float tile_space = 1f; // space b/w the tiles

    void Start()
    {
        generate_tiles();//generation of tiles when the game starts
    }

    private void generate_tiles()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Vector3 pos_tile = new Vector3(i * tile_space, 0, j * tile_space);
                GameObject tile_new = Instantiate(tile, pos_tile, Quaternion.identity); // create new gameobject
                tile_new.name = $"{i}_{j}"; // sets the name of tiles
                TilePos tile_info = tile_new.GetComponent<TilePos>();
                tile_info.SetCoordinates(i, j);
            }
        }
    }
}