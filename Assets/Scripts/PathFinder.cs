using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField]
    public bool b_AllowDiagonal;

    public Vector3Int StartNodePosition;
    public Vector3Int GoalNodePosition;

    private const int CROSS_COST = 10;
    private const int DIAGNAL_COST = 14;

    private Node CursorNode;
    private TilemapDatas TilemapDatas;
    private TilemapNodes TilemapNodes;    

    private List<Node> OpenList;
    private List<Node> CloseList;
    private List<Vector3Int> PathList;

    void Awake()
    {
        TilemapDatas = GetComponent<TilemapDatas>();
        TilemapNodes = GetComponent<TilemapNodes>();
    }

    public bool Find(Vector3Int startPosition, Vector3Int goalPosition)
    {
        OpenList = new List<Node>();
        CloseList = new List<Node>();
        PathList = new List<Vector3Int>();

        StartNodePosition = startPosition;
        Node StartNode = TilemapNodes.GetNodeOrNull(startPosition);
        if (StartNode == null)
        {
            return false;
        }

        GoalNodePosition = goalPosition;
        Node GoalNode = TilemapNodes.GetNodeOrNull(goalPosition);
        if (GoalNode == null)
        {
            return false;
        }

        OpenList.Add(StartNode);
        while (OpenList.Count > 0)
        {
            CursorNode = OpenList[0];

            for (int i = 1; i <  OpenList.Count; ++i)
            {
                // Tilemap에서는 F, G, H가 똑같은 값이 있다.
                if (OpenList[i].F <= CursorNode.F &&
                    OpenList[i].H <  CursorNode.H &&
                    OpenList[i].G <  CursorNode.G)
                {
                    CursorNode = OpenList[i];
                }
            }

            // 마지막 노드.
            if (CursorNode == GoalNode)
            {
                Node reverseCursorNode = GoalNode;

                while (reverseCursorNode != StartNode)
                {                    
                    PathList.Add(reverseCursorNode.Position);
                    reverseCursorNode = reverseCursorNode.ParentNode;
                }
                PathList.Add(StartNode.Position);
                PathList.Reverse();

                return true;
            }

            Vector3Int position = new Vector3Int();
            // ↑ → ↓ ←
            position = new Vector3Int(CursorNode.Position.x,
                                      CursorNode.Position.y + 1,
                                      CursorNode.Position.z);
            AddOpenList(position);

            position = new Vector3Int(CursorNode.Position.x + 1,
                                      CursorNode.Position.y,
                                      CursorNode.Position.z);
            AddOpenList(position);

            position = new Vector3Int(CursorNode.Position.x,
                                      CursorNode.Position.y - 1,
                                      CursorNode.Position.z);
            AddOpenList(position);

            position = new Vector3Int(CursorNode.Position.x - 1,
                                      CursorNode.Position.y,
                                      CursorNode.Position.z);
            AddOpenList(position);

            // ↗↘↙↖
            if (b_AllowDiagonal)
            {
                position = new Vector3Int(CursorNode.Position.x + 1,
                                          CursorNode.Position.y + 1,
                                          CursorNode.Position.z);
                AddOpenList(position);

                position = new Vector3Int(CursorNode.Position.x + 1,
                                          CursorNode.Position.y - 1,
                                          CursorNode.Position.z);
                AddOpenList(position);

                position = new Vector3Int(CursorNode.Position.x - 1,
                                          CursorNode.Position.y - 1,
                                          CursorNode.Position.z);
                AddOpenList(position);

                position = new Vector3Int(CursorNode.Position.x - 1,
                                          CursorNode.Position.y + 1,
                                          CursorNode.Position.z);
                AddOpenList(position);
            }

            OpenList.Remove(CursorNode);
            CloseList.Add(CursorNode);
        }

        return false;
    }

    public List<Vector3Int> GetPathList()
    {
        return PathList;
    }

    private void AddOpenList(Vector3Int nodePosition)
    {
        Node addNode = TilemapNodes.GetNodeOrNull(nodePosition);

        // 벽
        if (TilemapDatas.IsWall(nodePosition) == true)
            return;
        // 닫힌목록
        if (CloseList.Contains(addNode) == true)
            return;

        int gScore = 0;
        // 대각선 확인.
        if (CheckDiagonal(addNode))
        {
            gScore = DIAGNAL_COST;
        }
        else
        {
            gScore = CROSS_COST;
        }

        // 이미 오픈목록에 있으면 기존값과 비교해서 낮은 값을 적용.
        if (OpenList.Contains(addNode) == true)
        {
            int newG = CursorNode.G + gScore;
            int newH = (Mathf.Abs(addNode.Position.x - GoalNodePosition.x) +
                     Mathf.Abs(addNode.Position.y - GoalNodePosition.y)) * CROSS_COST;
            int newF = newG + newH;


            int index = OpenList.IndexOf(addNode);
            if (newF <= addNode.F && newH < addNode.H)
            {
                OpenList[index].G = newG;
                OpenList[index].H = newH;
                OpenList[index].ParentNode = CursorNode;
            }
        }
        // 기존 OpenList에 없으면 추가.
        else
        {
            addNode.G = CursorNode.G + gScore;
            addNode.H = (Mathf.Abs(addNode.Position.x - GoalNodePosition.x) +
                              Mathf.Abs(addNode.Position.y - GoalNodePosition.y)) * CROSS_COST;
            addNode.ParentNode = CursorNode;

            OpenList.Add(addNode);
        }
    }

    private bool CheckDiagonal(Node addNode)
    {
        if (Mathf.Abs(CursorNode.Position.x - addNode.Position.x) == 1 &&
            Mathf.Abs(CursorNode.Position.y - addNode.Position.y) == 1)
        {
            return true;
        }

        return false;
    }
}
