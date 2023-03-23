using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMotion : MonoBehaviour
{
    private GameObject player;
    public float speed = 4f;
    private Rigidbody2D rb;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (player == null) return;
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        
        rb.AddForce(direction*speed);

        if (rb.velocity.magnitude > speed) rb.velocity = rb.velocity.normalized * speed;
    }
}
