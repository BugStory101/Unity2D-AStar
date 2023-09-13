using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileLineViewer : MonoBehaviour
{
    private LineRenderer Line;

    private int LineIndex;
    private Tilemap Tilemap;

    void Awake()
    {
        Line = GetComponent<LineRenderer>();
        Tilemap = GetComponent<Mapmanager>().Tilemap;

        Line.positionCount = 0;
        Line.enabled = true;
    }

    public void ClearLine()
    {
        Line.positionCount = 0;
        LineIndex = 0;
    }

    public void SetLineVisible(bool visible)
    {
        Line.enabled = visible;
    }

    public void AddPosition(Vector3Int position)
    {
        ++Line.positionCount;

        Vector3 cellLocal = Tilemap.GetCellCenterLocal(position);
        Line.SetPosition(LineIndex, cellLocal);

        ++LineIndex;
    }

    public void AddPosition(Vector3 position)
    {
        ++Line.positionCount;

        Vector3Int vector3Int = new Vector3Int((int)position.x, (int)position.y, (int)position.z);
        Vector3 cellLocal = Tilemap.GetCellCenterLocal(vector3Int);
        Line.SetPosition(LineIndex, cellLocal);

        ++LineIndex;
    }

    public void AddPositionList(List<Vector3Int> positionList)
    {
        ClearLine();

        for (int i = 0; i < positionList.Count; ++i)
        {
            ++Line.positionCount;
            Vector3 cellLocal = Tilemap.GetCellCenterLocal(positionList[i]);
            Line.SetPosition(LineIndex, cellLocal);
            ++LineIndex;
        }
    }
}
