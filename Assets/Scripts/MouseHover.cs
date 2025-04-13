using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MouseHover : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject tile_UI;
    [SerializeField] private TextMeshProUGUI tile_text;
    private PlayerMovement player_movement;
    private TilePos current_Tile;

    public void SetPlayerMovement(PlayerMovement movement)
    {
        player_movement = movement; // player movement on the basis of spawn
    }

    void Start()
    {
        tile_UI.SetActive(false); // UI Hidden
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition); // raycast hit
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            TilePos tile = hit.collider.GetComponent<TilePos>(); // current hovered tile
            if (tile != null)
            {
                if (current_Tile != null && current_Tile != tile)
                {
                    current_Tile.Highlight(false, tile_UI, tile_text); // highlight tile
                }
                tile.Highlight(true, tile_UI, tile_text);
                current_Tile = tile;
                tile_text.text = $"X: {tile.GetX()} - Y: {tile.GetY()}"; // UI text
                if (Input.GetMouseButtonDown(0) && player_movement != null && !player_movement.IsMoving) // get input for player movement
                {
                    Vector2Int player_pos = new Vector2Int(tile.GetX(), tile.GetY());
                    player_movement.MoveToTile(player_pos);
                }
            }
        }
        else
        {
            if (current_Tile != null)
            {
                current_Tile.Highlight(false, tile_UI, tile_text);
                current_Tile = null;
            }
            tile_text.text = "";
            tile_UI.SetActive(false); // reset UI
        }
    }
}