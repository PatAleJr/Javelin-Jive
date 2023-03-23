using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public int health = 3;
    public GameObject[] hearts;

    public void takeDamage() {
        health--;
        hearts[health].SetActive(false);
        if (health <= 0) die();
    }

    public void die() {
        Destroy(gameObject);
    }
}
