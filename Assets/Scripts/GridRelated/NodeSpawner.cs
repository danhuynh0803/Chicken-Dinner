using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSpawner : MonoBehaviour {

    [Header("Script for node spawns")]
    public TextAsset spawnScript;

    [Header("Randomized grid generator")]
    public bool isRandomGrid;
    public int minRow = 3, maxRow = 10;
    public int minCol = 3, maxCol = 10;

    [Header("Nodes types to be spawned (fill with prefabs)")]
    public GameObject baseNode;
    public GameObject powerNode;
    public GameObject link1;
    public GameObject link2;
    public GameObject link3;
    public GameObject link4;
    public GameObject weaponNode;

    private char[] delimiterChars = { ' ', ',', '\t' };
    private float spawnTimer;
    
    void Start ()
    {
        if (!isRandomGrid)
        {
            // Populate our grid with the nodes within the text file
            ParseTextFileAndSetupNodes();
        }
        else
        {
            RandomSetupNodes();
        }
     
	}

    private void RandomSetupNodes()
    {
        Grid.instance.SetupGrid(Random.Range(minRow, maxRow), Random.Range(minCol, maxCol));

        for (int r = 0; r < Grid.instance.GetMaxRow(); ++r)
        {
            for (int c = 0; c < Grid.instance.GetMaxCol(); ++c)
            {
                GameObject spawnedNode;
                float prob = Random.Range(0.0f, 6.0f);

                if (prob <= 1.0f)
                {
                    spawnedNode = powerNode;
                }
                else if (prob <= 2.0f)
                {
                    spawnedNode = link2;
                }
                else if (prob <= 3.0f)
                {
                    spawnedNode = link3;
                }
                else if (prob <= 4.0f)
                {
                    spawnedNode = link4;
                }
                else if (prob <= 5.0f)
                {
                    spawnedNode = weaponNode;
                }
                else
                {
                    spawnedNode = null;
                }
                
                if (spawnedNode != null)
                {
                    Grid.instance.SpawnNode(spawnedNode, r, c);
                }
            }
        }
    }

    // Parse and add the words from the word text file into the word list
    private void ParseTextFileAndSetupNodes()
    {
        // Parse each line in the textfile
        string[] lines = spawnScript.text.Split('\n');

        // The first line of the level denotes row and column of the grid     
        string[] dimensions = lines[0].Split(delimiterChars);
        int maxRows = int.Parse(dimensions[0]);
        int maxCols = int.Parse(dimensions[1]);
        Grid.instance.SetupGrid(maxRows, maxCols);

        // Parse each letter and setup the nodes accordingly, where
        // p = Power node
        // # (2,3,4) = Linker node
        // w = weapon node
        // - = empty spot
        /*
        // Debug text file 
        foreach (string line in lines)
        {
            Debug.Log(line);
        }
        */
        ///////////////////

        for (int r = 1; r < lines.Length; ++r)
        {
            //Debug.Log(lines[r]);

            string[] nodes = lines[r].Split(delimiterChars);
            // Debug parsed nodes
            /*
            foreach (string key in nodes)
            {
                Debug.Log(key);
            }
            */

            if (nodes == null) continue;
            for (int c = 0; c < nodes.Length; ++c)
            {
                GameObject spawnedNode;
                string nodeKey = nodes[c].ToLower();
                nodeKey = nodeKey.Trim();
                //Debug.Log("current key:" + nodeKey);
                if (nodeKey.Equals("p"))
                    spawnedNode = powerNode;
                else if (nodeKey.Equals("1"))
                    spawnedNode = link1;
                else if (nodeKey.Equals("2"))
                    spawnedNode = link2;
                else if (nodeKey.Equals("3"))
                    spawnedNode = link3;
                else if (nodeKey.Equals("4"))
                {     
                    spawnedNode = link4;
                }
                else if (nodeKey.Equals("w"))
                {
                    //Debug.Log("Entered w \t" + weaponNode);
                    spawnedNode = weaponNode;
                }
                else if (nodeKey.Equals("b"))
                {
                    spawnedNode = baseNode;
                }
                else if (nodeKey.Equals("s"))
                {
                    //Grid.instance.MoveStartPosition(c, maxRows - r);
                    spawnedNode = null;
                }
                else
                    spawnedNode = null;

                if (spawnedNode != null)
                {
                    Grid.instance.SpawnNode(spawnedNode, maxRows - r, c);
                }
                else
                {
                    //Debug.Log("Key:" + nodeKey + " has no object to spawn at row:" + (maxRows - r) + " col:" + c);
                }
            }                                      
        }

        /*
        for (int r = 0; r < Grid.instance.grid.GetLength(0); ++r)
        {
            for (int c = 0; c < Grid.instance.grid.GetLength(1); ++c)
            {
                if(Grid.instance.grid[r, c] != null)
                    Grid.instance.grid[r, c].gameObject.transform.position -= Grid.instance.startPos.position;
            }
        }
        */
    }
}
