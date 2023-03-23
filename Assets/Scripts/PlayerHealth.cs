using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public int health = 3;
    public GameObject[] hearts;

    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void takeDamage() {
        health--;
        hearts[health].SetActive(false);
        if (health <= 0) die();
        ScreenShake.instance.shake(0.1f, 0.2f);
        rb.velocity = Vector3.zero;
    }

    public void die() {
        Destroy(gameObject);
    }
}
