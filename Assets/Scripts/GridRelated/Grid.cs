using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    #region Singleton 
    public static Grid instance = null;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }        
    }
    #endregion

    private int maxRow, maxCol;

    public bool drawDebugLines;
    public float nodeOffset;
    public Transform startPos;

    public Node[,] grid;    

    public int GetMaxRow() { return maxRow; }
    public int GetMaxCol() { return maxCol; }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        for (int r = 0; r < grid.GetLength(0); ++r)
        {
            for (int c = 0; c < grid.GetLength(1); ++c)
            {
                Vector3 position = 
                    new Vector3(r * nodeOffset,
                                c * nodeOffset,
                                -1.0f);

                Gizmos.DrawSphere(position, 0.01f);
            }
        }
    }
    */

    public void SetupGrid(int maxRow, int maxCol)
    {
        this.maxRow = maxRow;
        this.maxCol = maxCol;

        grid = new Node[maxRow, maxCol];
        for (int r = 0; r < grid.GetLength(0); ++r)
        {
            for (int c = 0; c < grid.GetLength(1); ++c)
            {
                grid[r, c] = null;
            }
        }
    }

    public void SpawnNode(GameObject goNode, int row, int col)
    {
        //Debug.Log(goNode.name);
        //Debug.Log("row: " + row + "\tcol:" + col);
        if (goNode != null)
        {
            Vector3 position = new Vector3(col * nodeOffset,
                                           row * nodeOffset,
                                           -1.0f);

            position += startPos.position;

            GameObject nodeInst =
                Instantiate(goNode, position, Quaternion.identity) as GameObject;

            // This object contains a child object that has the node object
            // So we need to reference that child as opposed to just using the parent
            // These objects are typically the ones that have healthbars to avoid rotating the healthbars                  
            
            if (goNode.name.ToLower().Contains("parent"))
            {                                
                GameObject childNode = goNode.GetComponentInChildren<Node>().gameObject;
                if (childNode != null)
                {
                    //Debug.Log(childNode.gameObject.name);
                    childNode.gameObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
                    childNode.GetComponent<Node>().SetPosition(row, col);
                    grid[row, col] = childNode.GetComponent<Node>();
                }               
            }
            else
            {
                nodeInst.GetComponent<Node>().SetPosition(row, col);

                grid[row, col] = nodeInst.GetComponent<Node>();
            }
            
        }
    }

    /*
    // Performed BFS starting from each power node and turns on all connected neighbors
    public void ReconnectLinkerNodes()
    {
        TurnOffAllNodes();

        PowerNode[] powerNodes = FindObjectsOfType<PowerNode>();

        foreach (PowerNode power in powerNodes)
        {
            IterativeBFS(power);
        }
    }

    // Used prior to starting BFS, since we want to reevaluate which nodes should be turned on
    public void TurnOffAllNodes()
    {
        for (int r = 0; r < grid.GetLength(0); ++r)
        {
            for (int c = 0; c < grid.GetLength(1); ++c)
            {
                // Do not disable power node
                if (grid[r, c] != null && grid[r, c].GetComponent<PowerNode>() == null)
                {                  
                    grid[r, c].isOn = false;
                }
            }
        }
    }

    public void IterativeBFS(LinkerNode root)
    {
        Queue<LinkerNode> queue = new Queue<LinkerNode>();
        queue.Enqueue(root);

        bool[,] visitedGrid = new bool[maxRow, maxCol];
        visitedGrid[root.row, root.col] = true;

        int loopCount = 0; // Used to protect against infinite loops

        while (queue.Count > 0 && loopCount < 100)
        {
            loopCount++;

            LinkerNode node = queue.Dequeue();

            List<LinkerNode> listOfLinkers = node.GetListOfConnectedLinkerNodes();

            foreach(LinkerNode linkNode in listOfLinkers)
            {                
                if (!visitedGrid[linkNode.row, linkNode.col])
                {
                    // Node is newly visited so add it to our queue and turn it on
                    // since it is a connected neighbor based on it's activators
                    queue.Enqueue(linkNode);
                    linkNode.isOn = true;
                    visitedGrid[linkNode.row, linkNode.col] = true;
                }
            }
        }
    }

    public void MoveStartPosition(float x, float y)
    {
        startPos.position = new Vector3(x, y, startPos.position.z);
    }
    */
}
