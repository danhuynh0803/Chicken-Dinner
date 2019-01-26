using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node : MonoBehaviour{

    public bool isOn;       // Determines if node is active

    public int row, col;

    // Debugging, color green if node is on
    public void Update()
    {
        if (isOn)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }   

    public void SetPosition(int row, int col)
    {
        this.row = row;
        this.col = col;
    }

    // Not really useful, maybe rethink or delete later
    public List<Node> GetAllNeighborNodes()
    {
        List<Node> neighborNodes = new List<Node>();

        for (int dir = 0; dir < 4; ++dir)
        {
            // 0 = North, 1 = east, 2 = south, 3 = west
            Node neighbor = GetNextNodeByDirection(dir);
            if (neighbor != null)
            {
                neighborNodes.Add(neighbor);
            }
        }

        return neighborNodes;
    }

    public Node GetNextNodeByDirection(int direction)
    {
        Node nextNode = null;
        //Debug.Log("This node row:" + row + "\tcol:" + col);
        // Check North
        if (direction == 0)
        {
            for (int r = row + 1; r < Grid.instance.GetMaxRow(); ++r)
            {
                if (Grid.instance.grid[r, col] != null)
                {
                    //Debug.Log("Found North");
                    nextNode = Grid.instance.grid[r, col];
                    break;
                }
            }
        }
        // Check East
        else if (direction == 1)
        {
            for (int c = col + 1; c < Grid.instance.GetMaxCol(); ++c)
            {
                if (Grid.instance.grid[row, c] != null)
                {
                    //Debug.Log("Found East");
                    nextNode = Grid.instance.grid[row, c];
                    break;
                }
            }
        }
        // Check South
        else if (direction == 2)
        {
            for (int r = row - 1; r >= 0; --r)
            {
                if (Grid.instance.grid[r, col] != null)
                {
                    //Debug.Log("Found South");
                    nextNode = Grid.instance.grid[r, col];
                    break;
                }
            }
        }
        // Check West
        else if (direction == 3)
        {
            for (int c = col - 1; c >= 0; --c)
            {
                if (Grid.instance.grid[row, c] != null)
                {
                    //Debug.Log("Found West");
                    nextNode = Grid.instance.grid[row, c];
                    break;
                }
            }
        }

        if (nextNode != null)
        {
            //Debug.Log("nextNode row:" + nextNode.row + "\tcol:" + nextNode.col);
        }
        return nextNode;
    }
}
