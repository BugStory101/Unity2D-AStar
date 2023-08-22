using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mapmanager : MonoBehaviour
{
    [SerializeField]
    public Tilemap Tilemap;

    public Dictionary<TileBase, TileData> DataFromTiles;
    [SerializeField]
    private List<TileData> m_TileDatas;

    public void Awake()
    {
        Tilemap.CompressBounds();
        DataFromTiles = new Dictionary<TileBase, TileData>();

        foreach (TileData tileData in m_TileDatas)
        {
            foreach (TileBase tileBase in tileData.Tiles)
            {
                DataFromTiles.Add(tileBase, tileData);
            }
        }
    }

    public void LateUpdate()
    {
        if(Input.GetMouseButtonUp(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = Tilemap.WorldToCell(mousePosition);
            TileBase clickTile = Tilemap.GetTile(gridPosition);

            if (clickTile == null)
                return;

            if (DataFromTiles[clickTile].IsWall)
            {
                Debug.Log("IsWall True");
            }
            else
            {
                Debug.Log("IsWall False");
            }
        }      
    }




}
