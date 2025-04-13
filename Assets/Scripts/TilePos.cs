using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TilePos : MonoBehaviour
{
    private int posX;
    private int posY;

    private Renderer rend; // for getting the rendered tile
    private Material tile_Mat;
    private Color original_emit; // the original emission for reverting it
    private Color hover_emit = Color.blue * 1.5f;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        tile_Mat = new Material(rend.material); // get the tile material
        rend.material = tile_Mat;

        if (tile_Mat.HasProperty("_EmissionColor"))
        {
            original_emit = tile_Mat.GetColor("_EmissionColor");
        }
        tile_Mat.EnableKeyword("_EMISSION");
    }

    public void SetCoordinates(int x, int y)
    {
        this.posX = x;
        this.posY = y;
    }

    public int GetX() => posX;
    public int GetY() => posY;

    public void Highlight(bool is_hovering, GameObject tile_UI, TextMeshProUGUI tile_text) // highlight the hovered tile in blue color + set material
    {
        if (tile_Mat != null)
        {
            if (is_hovering)
            {
                tile_Mat.SetColor("_EmissionColor", original_emit + hover_emit);
                tile_text.text = $"X: {posX} Y: {posY}"; // UI Text
                tile_UI.SetActive(true); // UI Shown
            }
            else
            {
                tile_Mat.SetColor("_EmissionColor", original_emit);
                tile_UI.SetActive(false); // UI Hidden
            }
        }
    }
}
