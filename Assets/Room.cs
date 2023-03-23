using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject[] bottomWalls;
    public GameObject[] topWalls;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            GameManager.instance.EnteredNextRoom();
        Destroy(GetComponent<Collider2D>());
    }
    public void destroyBottomWalls() {
        for (int i = 0; i < bottomWalls.Length; i++) {
            Destroy(bottomWalls[i]);
        }
    }

    public void deactivateTopWalls() {
        for (int i = 0; i < bottomWalls.Length; i++)
        {
            topWalls[i].SetActive(false);
        }
    }
    public void activateTopWalls()
    {
        for (int i = 0; i < bottomWalls.Length; i++)
        {
            topWalls[i].SetActive(true);
        }
    }
}
