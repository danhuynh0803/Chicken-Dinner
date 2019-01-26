using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathNode : MonoBehaviour {

    public GameObject basePrefab;
    public Vector3[] wayPoints;
    public GameObject enemyPathTilePrefab;
    public GameObject enemyPathCornerTilePrefab;
    public bool isDebug = true;
    public float radius;

    public int Length
    {
        get { return wayPoints.Length; }
    }

    public Vector3 GetPoint(int index)
    {
        return wayPoints[index];
    }

    private void OnDrawGizmos()
    {
        if (!isDebug)
        {
            return;
        }
        for (int i = 0; i < wayPoints.Length; i++)
        {
            if (i + 1 < wayPoints.Length)
            {
                Debug.DrawLine(wayPoints[i], wayPoints[i + 1], Color.red);
            }
        }
    }

    private void Start()
    {
        for (int i = 0; i < wayPoints.Length; i++)
        {
            if (i + 1 < wayPoints.Length)
            {
                float deltaX = (wayPoints[i + 1].x - wayPoints[i].x);
                float deltaY = (wayPoints[i + 1].y - wayPoints[i].y);
                if (deltaX != 0)
                {
                    for (int j = 0; j < Mathf.Abs(deltaX); j++)
                    {

                        if (j == 0 && i != 0)
                        {
                            if (wayPoints[i].y - wayPoints[i - 1].y > 0)
                            {
                                if(deltaX > 0)
                                {
                                    GameObject enemyPathTile = Instantiate(enemyPathCornerTilePrefab,
                                        wayPoints[i] + Vector3.forward,
                                        Quaternion.Euler(180f, -90f, 90f));
                                }
                                else
                                {
                                    GameObject enemyPathTile = Instantiate(enemyPathCornerTilePrefab,
                                       wayPoints[i] + Vector3.forward,
                                       Quaternion.Euler(90f, -90f, 90f));
                                }
                            }
                            else
                            {
                                if (deltaX > 0)
                                {
                                    GameObject enemyPathTile = Instantiate(enemyPathCornerTilePrefab,
                                        wayPoints[i] + Vector3.forward,
                                        Quaternion.Euler(270f, -90f, 90f));
                                }
                                else
                                {
                                    GameObject enemyPathTile = Instantiate(enemyPathCornerTilePrefab,
                                       wayPoints[i] + Vector3.forward,
                                       Quaternion.Euler(0f, -90f, 90f));
                                }
                            }
                        }
                        else
                        {
                            GameObject enemyPathTile = Instantiate(enemyPathTilePrefab,
                            wayPoints[i] + j * new Vector3(deltaX / Mathf.Abs(deltaX), 0f, 0f) + Vector3.forward,
                            Quaternion.Euler(-90f, 0f, 0f));
                        }
                        
                    }
                }
                if (deltaY != 0)
                {
                    for (int j = 0; j < Mathf.Abs(deltaY); j++)
                    {
                        if( j == 0 && i != 0)
                        {
                            if (wayPoints[i].x - wayPoints[i - 1].x > 0)
                            {
                                if (deltaY > 0)
                                {
                                    GameObject enemyPathTile = Instantiate(enemyPathCornerTilePrefab,
                                        wayPoints[i] + Vector3.forward,
                                        Quaternion.Euler(0f, -90f, 90f));
                                }
                                else
                                {
                                    GameObject enemyPathTile = Instantiate(enemyPathCornerTilePrefab,
                                       wayPoints[i] + Vector3.forward,
                                       Quaternion.Euler(90f, -90f, 90f));
                                }
                            }
                            else
                            {
                                if (deltaY > 0)
                                {
                                    GameObject enemyPathTile = Instantiate(enemyPathCornerTilePrefab,
                                        wayPoints[i] + Vector3.forward,
                                        Quaternion.Euler(270f, -90f, 90f));
                                }
                                else
                                {
                                    GameObject enemyPathTile = Instantiate(enemyPathCornerTilePrefab,
                                       wayPoints[i] + Vector3.forward,
                                       Quaternion.Euler(180f, -90f, 90f));
                                }
                            }
                        }
                        else
                        {
                            GameObject enemyPathTile = Instantiate(enemyPathTilePrefab,
                            wayPoints[i] + j * new Vector3(0f, deltaY / Mathf.Abs(deltaY), 0f) + Vector3.forward,
                            Quaternion.Euler(-180f, -90f, 90f));
                        }
                    }
                }

            }
            else
            {
                GameObject enemyPathTile = Instantiate(enemyPathTilePrefab,
                        wayPoints[Length-1] + Vector3.forward,
                        Quaternion.identity);

                GameObject CommandCenterObj =
                    Instantiate(basePrefab, wayPoints[Length - 1] + Vector3.forward, Quaternion.identity)
                    as GameObject;
            }



        }
       
    }
}
