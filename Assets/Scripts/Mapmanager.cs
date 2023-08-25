using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mapmanager : MonoBehaviour
{
    [SerializeField]
    public Tilemap Tilemap;


    public void Awake()
    {
        Tilemap.CompressBounds();
    }

    public void LateUpdate()
    {
        if(Input.GetMouseButtonUp(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = Tilemap.WorldToCell(mousePosition);
            TileBase clickTile = Tilemap.GetTile(gridPosition);

        }      
    }




}
