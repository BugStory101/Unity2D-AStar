using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mapmanager : MonoBehaviour
{
    [SerializeField]
    public Tilemap Tilemap;

    private bool mb_ClickedStart = false;
    private bool mb_ClickedGoal = false;
    private Vector3Int m_StartPosition;
    private Vector3Int m_GoalPosition;

    [SerializeField]
    private TileBase StartTileBase;
    [SerializeField]
    private TileBase GoalTileBase;

    private TileBase SaveStartTileBase;
    private TileBase SaveGoalTileBase;

    private TilemapDatas TilemapDatas;

    private PathFinder PathFinder;


    public void Awake()
    {
        Tilemap.CompressBounds();
        TilemapDatas = GetComponent<TilemapDatas>();
        PathFinder = GetComponent<PathFinder>();
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
        TileBase tileBase = Tilemap.GetTile(gridPosition);

        if (TilemapDatas.IsWall(tileBase))
        {
            return;
        }

        // Click Off
        if (m_StartPosition == gridPosition)
        {
            m_StartPosition = new Vector3Int(9999, 9999);
            Tilemap.SetTile(gridPosition, SaveStartTileBase);
            mb_ClickedStart = false;

            return;
        }
        if (m_GoalPosition == gridPosition)
        {
            m_GoalPosition = new Vector3Int(-9999, -9999);
            Tilemap.SetTile(gridPosition, SaveGoalTileBase);
            mb_ClickedGoal = false;

            return;
        }

        // Click On
        if (mb_ClickedStart == false)
        {
            m_StartPosition = gridPosition;
            SaveStartTileBase = Tilemap.GetTile(gridPosition);
            Tilemap.SetTile(gridPosition, StartTileBase);
            mb_ClickedStart = true;

            return;
        }
        if (mb_ClickedGoal == false)
        {
            m_GoalPosition = gridPosition;
            SaveGoalTileBase = Tilemap.GetTile(gridPosition);
            Tilemap.SetTile(gridPosition, GoalTileBase);
            mb_ClickedGoal = true;

            return;
        }
    }

    private void MouseRightClickUp()
    {
        if(mb_ClickedStart && mb_ClickedGoal)
        {
            if (PathFinder.Find(m_StartPosition, m_GoalPosition) == false)
                return;

            List<Vector3Int> PathList = PathFinder.GetPathList();
            for (int i = 0; i < PathList.Count; ++i)
            {
                Debug.Log(PathList[i]);
            }
        }
    }
}
