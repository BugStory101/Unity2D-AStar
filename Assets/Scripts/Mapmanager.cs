using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mapmanager : MonoBehaviour
{
    [SerializeField]
    public Tilemap Tilemap;

    private bool b_ClickedStart = false;
    private bool b_ClickedGoal = false;
    private Vector3Int StartPosition;
    private Vector3Int GoalPosition;

    [SerializeField]
    private TileBase StartTileBase;
    [SerializeField]
    private TileBase GoalTileBase;

    private TileBase SaveStartTileBase;
    private TileBase SaveGoalTileBase;

    private TilemapDatas TilemapDatas;
    private TilemapNodes TilemapNodes;
    private TileLineViewer LineViewer;
    private TileTextViewer TileTextViewer;
    private PathFinder PathFinder;    


    void Awake()
    {
        Tilemap.CompressBounds();
        TilemapDatas = GetComponent<TilemapDatas>();
        TilemapNodes = GetComponent<TilemapNodes>();
        PathFinder = GetComponent<PathFinder>();
        LineViewer = GetComponent<TileLineViewer>();
        TileTextViewer = GetComponent<TileTextViewer>();
    }

    private void LateUpdate()
    {
        // Mouse Left Up
        if (Input.GetMouseButtonUp(0))
        {
            MouseLeftClickUp();
        }

        if(Input.GetMouseButtonUp(1))
        {
            MouseRightClickUp();
        }
    }

    private void MouseLeftClickUp()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPosition = Tilemap.WorldToCell(mousePosition);
        Node addNode = TilemapNodes.GetNodeOrNull(gridPosition);

        // Tilemap 범위 밖이면
        if (addNode == null)
            return;

        TileBase tileBase = Tilemap.GetTile(gridPosition);

        if (TilemapDatas.IsWall(tileBase))
        {
            return;
        }

        // Click Off
        if (StartPosition == gridPosition)
        {
            LineViewer.SetLineVisible(false);

            StartPosition = new Vector3Int(9999, 9999);
            Tilemap.SetTile(gridPosition, SaveStartTileBase);
            b_ClickedStart = false;

            return;
        }
        if (GoalPosition == gridPosition)
        {
            LineViewer.SetLineVisible(false);

            GoalPosition = new Vector3Int(-9999, -9999);
            Tilemap.SetTile(gridPosition, SaveGoalTileBase);
            b_ClickedGoal = false;

            return;
        }

        // Click On
        if (b_ClickedStart == false)
        {
            StartPosition = gridPosition;
            SaveStartTileBase = Tilemap.GetTile(gridPosition);
            Tilemap.SetTile(gridPosition, StartTileBase);
            b_ClickedStart = true;

            return;
        }
        if (b_ClickedGoal == false)
        {
            GoalPosition = gridPosition;
            SaveGoalTileBase = Tilemap.GetTile(gridPosition);
            Tilemap.SetTile(gridPosition, GoalTileBase);
            b_ClickedGoal = true;

            return;
        }
    }

    private void MouseRightClickUp()
    {
        if ((b_ClickedStart == false) || (b_ClickedGoal == false))
            return;

        // Find
        PathFinder.Find(StartPosition, GoalPosition);
    }
}
