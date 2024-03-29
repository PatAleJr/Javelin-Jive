using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 10;
    public float damageFlashTime = 0.1f;

    public float knockback = 5f;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private AudioSource audioSource;

    public GameObject deathParticles;
    public GameObject hitParticles;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        setColorAccordingToHealth();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<PlayerHealth>().takeDamage();
            die();
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

        rb.AddForce(direction * knockback);

        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.Play();

        if (health <= 0) die();
    }

    IEnumerator resetColor() {
        yield return new WaitForSeconds(damageFlashTime);
        setColorAccordingToHealth();
    }

    public void die() {
        GameObject parts = Instantiate(deathParticles);
        parts.transform.position = transform.position;
        ScreenShake.instance.shake(0.05f, 0.1f);
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
