using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public int health = 10;
    public float damageFlashTime = 0.1f;

    private SpriteRenderer sr;

    public GameObject deathParticles;
    public GameObject hitParticles;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();

        GetComponent<AIDestinationSetter>().target = GameObject.Find("Player").transform;

        setColorAccordingToHealth();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<PlayerHealth>().takeDamage();
            Destroy(gameObject);
        }
    }

    public void takeDamage(int damage, Vector3 direction) {
        health -= damage;

        GameObject parts = Instantiate(hitParticles);
        parts.transform.position = transform.position;
        var main = parts.GetComponent<ParticleSystem>().main;
        main.startColor = sr.color;
        parts.transform.rotation = Quaternion.LookRotation(direction);

        sr.color = Color.white;
        StartCoroutine(resetColor());

        if (health <= 0) die();
    }

    IEnumerator resetColor() {
        yield return new WaitForSeconds(damageFlashTime);
        setColorAccordingToHealth();
    }

    private void die() {
        GameObject parts = Instantiate(deathParticles);
        parts.transform.position = transform.position;
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
