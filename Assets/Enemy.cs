using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 1;

    public float damageFlashTime = 0.1f;

    private SpriteRenderer sr;
    private Color initialColor;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        initialColor = sr.color;
    }

    public void takeDamage(int damage) {
        health -= damage;

        sr.color = Color.white;
        StartCoroutine(resetColor());

        if (health <= 0) die();
    }

    IEnumerator resetColor() {
        yield return new WaitForSeconds(damageFlashTime);
        sr.color = initialColor;
    }

    private void die() { 
        Destroy(gameObject);
    }
}
