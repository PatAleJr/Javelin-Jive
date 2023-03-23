using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 direction = Vector3.zero;
    public GameObject spearWallPrefab;
    void FixedUpdate()
    {
        float move = speed * Time.deltaTime;
        transform.Translate(move*direction, Space.World);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall") {

            Collider2D collider = collision.collider;
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 center = collider.bounds.center;
            bool right = contactPoint.x > center.x;
            bool top = contactPoint.y > center.y;

            float posX = 0;
            float posY = 0;
            if (direction.x != 0) posX = right ? 1 : -1;
            if (direction.y != 0) posY = top ? 1 : -1;

            Vector3 relativePosition = new Vector3(posX, posY, 0);
            Vector3 newWallPosition = collision.gameObject.transform.position + relativePosition;
            Transform wall = Instantiate(spearWallPrefab).transform;
            wall.position = newWallPosition;
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "WallPlayer") {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().takeDamage(1, direction);
            Destroy(gameObject);
        }
    }
}
