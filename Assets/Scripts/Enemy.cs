using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public int health = 10;
    public float damageFlashTime = 0.1f;

    private SpriteRenderer sr;
    private Color initialColor;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        initialColor = sr.color;

        GetComponent<AIDestinationSetter>().target = GameObject.Find("Player").transform;

        setColorAccordingToHealth();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.tag == "Player") {
            Debug.Log("With player");
            collision.gameObject.GetComponent<PlayerHealth>().takeDamage();
            Destroy(gameObject);
        }
    }

    public void takeDamage(int damage) {
        health -= damage;

        sr.color = Color.white;
        StartCoroutine(resetColor());

        if (health <= 0) die();
    }

    IEnumerator resetColor() {
        yield return new WaitForSeconds(damageFlashTime);
        setColorAccordingToHealth();
    }

    private void die() { 
        Destroy(gameObject);
    }

    public void setColorAccordingToHealth() {
        int maxHealth = 10;
        float hue = (float)health/(float)maxHealth *0.7f;

        hue -= 0.05f;
        if (hue < 0) hue += 1;

        sr.color = Color.HSVToRGB(hue, 0.8f, 1f);
    }
}
