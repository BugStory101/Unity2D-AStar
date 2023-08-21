using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mapmanager : MonoBehaviour
{
    [SerializeField]
    public Tilemap Tilemap;

    public Dictionary<TileBase, TileData> m_DataFromTiles;
    [SerializeField]
    private List<TileData> m_TileDatas;

    public void Awake()
    {
        Tilemap.CompressBounds();
        m_DataFromTiles = new Dictionary<TileBase, TileData>();

        foreach (TileData tileData in m_TileDatas)
        {
            foreach (TileBase tileBase in tileData.Tiles)
            {
                m_DataFromTiles.Add(tileBase, tileData);
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

            if (m_DataFromTiles[clickTile].IsWall)
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
