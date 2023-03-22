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

    public void Update()
    {
        if (shootingCnt <= 0) {
            if (Input.GetKeyUp(KeyCode.UpArrow))
                shoot(Vector3.up);
            if (Input.GetKeyUp(KeyCode.DownArrow))
                shoot(Vector3.down);
            if (Input.GetKeyUp(KeyCode.LeftArrow))
                shoot(Vector3.left);
            if (Input.GetKeyUp(KeyCode.RightArrow))
                shoot(Vector3.right);
        }
        shootingCnt -= Time.deltaTime;
    }
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        float moveHor = speed*Time.deltaTime*horizontalInput;
        float moveVer = speed*Time.deltaTime*verticalInput;
        movement = new Vector3(moveHor, moveVer, 0f);
        transform.Translate(movement);
    }

    private void shoot(Vector3 direction) {
        GameObject spear = Instantiate(spearPrefab);
        spear.transform.position = transform.position + direction;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        spear.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        spear.GetComponent<Spear>().direction = direction;

        shootingCnt = shootingCooldown;
    }
}
