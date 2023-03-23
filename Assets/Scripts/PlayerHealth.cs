using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;
    public GameObject[] hearts;

    public void takeDamage()
    {
        //get empty on player called HurtNoise
        //play sound
        //GameObject.Find("HurtNoise").GetComponent<AudioSource>().Play();

        health--;
        hearts[health].SetActive(false);
        if (health <= 0)
            die();
        ScreenShake.instance.shake(0.1f, 0.2f);
    }

    public void die()
    {
        Destroy(gameObject);
    }
}
