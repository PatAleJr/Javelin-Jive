using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 movement = Vector3.zero;
    public GameObject spearPrefab;

    public float shootingCooldown = 0.5f;
    private float shootingCnt = 0f;

    private Transform GFX;
    private Animator ac;

    public void Start()
    {
        GFX = transform.GetChild(0);
        ac = transform.GetComponentInChildren<Animator>();
    }

    public void Update()
    {
        if (shootingCnt <= 0)
        {
            // allow 8 way shooting
            float[] shootDirection = new float[2];
            if (Input.GetKey(KeyCode.UpArrow))
                shootDirection[1] += 1;
            if (Input.GetKey(KeyCode.DownArrow))
                shootDirection[1] -= 1;
            if (Input.GetKey(KeyCode.LeftArrow))
                shootDirection[0] -= 1;
            if (Input.GetKey(KeyCode.RightArrow))
                shootDirection[0] += 1;

            shoot(new Vector3(shootDirection[0], shootDirection[1], 0f));
        } else {
            shootingCnt -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        float moveHor = speed * Time.deltaTime * horizontalInput;
        float moveVer = speed * Time.deltaTime * verticalInput;

        ac.SetBool("Sideways", horizontalInput != 0);
        ac.SetBool("Down", moveVer < 0);
        ac.SetBool("Up", moveVer > 0);

        if (horizontalInput < 0)
        {
            GFX.localScale = new Vector3(-1, 1, 1);
        }
        else if (horizontalInput > 0)
        {
            GFX.localScale = new Vector3(1, 1, 1);
        }

        movement = new Vector3(moveHor, moveVer, 0f);
        transform.Translate(movement);

        ac.speed = movement == Vector3.zero ? 0f : 1f;
    }

    private void shoot(Vector3 direction)
    {
        if (direction == Vector3.zero)
            return;

        GameObject spear = Instantiate(spearPrefab);
        spear.transform.position = transform.position + direction;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        spear.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        spear.GetComponent<Spear>().direction = direction;

        // play shoot noise from audio source
        // audio source is a child of the player
        GetComponent<AudioSource>().Play();

        // randomize the pitch of the audio source
        GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);

        shootingCnt = shootingCooldown;
    }

    float spearWallTimer = 0f;
    public float spearWallDestroyTime = 0.5f;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "SpearWall")
        {
            Debug.Log("Hitting Spear Wall");
            spearWallTimer += Time.deltaTime;

            if (spearWallTimer >= spearWallDestroyTime)
            {
                spearWallTimer = 0f;
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "SpearWall")
        {
            spearWallTimer = 0f;
        }
    }
}
