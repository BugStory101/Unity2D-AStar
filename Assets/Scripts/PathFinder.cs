using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{

    public Vector3Int StartNodePosition;
    public Vector3Int GoalNodePosition;

    private const int CROSS_CONST = 10;
    private const int DIAGNAL_COST = 14;

    private Node CursorNode;
    private Mapmanager Mapmanager;

    private List<Node> OpenList;
    private List<Node> CloseList;


    public void Awake()
    {
        Mapmanager = GetComponent<Mapmanager>();
    }

    public bool Find(Vector3Int startPosition, Vector3Int goalPosition)
    {
        OpenList = new List<Node>();
        CloseList = new List<Node>();

        StartNodePosition = startPosition;
        /*
        Node StartNode = Mapmanager.GetNodeOrNull(StartPosition);
        if (StartNode == null)
        {
            print("StartNode not found");
            return false;
        }

        GoalPosition = goalPosition;
        Node GoalNode = m_Mapmanager.GetNodeOrNull(GoalPosition);
        if (GoalNode == null)
        {
            print("GoalNode not found");
            return false;
        }
        */






        return true;
    }



}
