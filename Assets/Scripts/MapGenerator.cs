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
                if (grid[xx, yy]) {
                    if (countNeighbors(xx, yy) >= 1)
                    {
                        grid[xx, yy] = true;
                    }
                    else
                    {
                        grid[xx, yy] = false;
                    }
                }
                else {
                    if (countNeighbors(xx, yy) >= 3)
                        grid[xx, yy] = true;
                }
            }
        }

        //Guarantee a way in and out
        grid[7, 0] = false;
        grid[9, 0] = false;
        grid[7, 1] = false;
        grid[9, 1] = false;
        for (int i = 0; i < tilesY; i++) {
            grid[8, i] = false;
        }

        for (int xx = 1; xx < tilesX - 1; xx++) {
            for (int yy = 1; yy < tilesY - 1; yy++) {
                if (grid[xx, yy]) {
                    GameObject newWall = Instantiate(wallPrefab, transform);
                    newWall.transform.localPosition = new Vector3(xx - tilesX / 2, yy - tilesY / 2);
                }
            }
        }

        int countNeighbors(int xx, int yy)
        {
            int cnt = 0;
            if (grid[xx - 1, yy]) cnt++;
            if (grid[xx + 1, yy]) cnt++;
            if (grid[xx, yy - 1]) cnt++;
            if (grid[xx, yy + 1]) cnt++;
            return cnt;
        }
    }
}
