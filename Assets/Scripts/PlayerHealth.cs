using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;
    public GameObject[] hearts;
    public float damageCooldownTime = 0.5f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    bool invincible = false;

    public void takeDamage()
    {
        if (!invincible)
        {
            health--;
            hearts[health].SetActive(false);
            if (health <= 0)
                die();
            ScreenShake.instance.shake(0.1f, 0.2f);
            rb.velocity = Vector3.zero;
            invincible = true;
        }
    }

    public void die()
    {
        GameManager.instance.GameOver();
        Destroy(gameObject);
    }

    // update is called once per frame
    float timeSinceLastDamage = 0.0f;

    private void Update()
    {
        if (invincible)
        {
            timeSinceLastDamage += Time.deltaTime;
            if (timeSinceLastDamage >= damageCooldownTime)
            {
                invincible = false;
                timeSinceLastDamage = 0.0f;
            }
        }
    }
}
