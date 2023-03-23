using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int tilesX = 23;
    public int tilesY = 17;
    public float density = 0.4f;

    public GameObject wallPrefab;

    void Start()
    {
        bool[,] grid = new bool[tilesX,tilesY];

        for (int xx = 0; xx < tilesX; xx++) { 
            for (int yy = 0; yy < tilesY; yy++) { 
                if ((float)Random.Range(0f, 1f) < density) {
                    grid[xx,yy] = true;
                } else {
                    grid[xx,yy] = false;
                }
            }
        }

        for (int xx = 1; xx < tilesX-1; xx++) { 
            for (int yy = 1; yy < tilesY-1; yy++) { 
                if (grid[xx,yy]) {
                    if (grid[xx-1,yy] || grid[xx+1,yy] || grid[xx,yy-1] || grid[xx,yy+1]) {
                        grid[xx, yy] = true;
                        GameObject newWall = Instantiate(wallPrefab);
                        newWall.transform.position = new Vector3(xx - tilesX/2, yy - tilesY/2);
                    } else {
                        grid[xx,yy] = false;
                    }
                }
            }
        }
    }

    void countNeighbors(int xx, int yy) {
        
    }
}
