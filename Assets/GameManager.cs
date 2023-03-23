using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool roundIsActive;
    public int round;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI roundText;

    private float time;
    public float roundTime = 45;

    public void BeginRound()
    {
        roundIsActive = true;
        time = roundTime;

        roundText.text = "Round " + round;
    }

    public void Start()
    {
        BeginRound();
    }

    public void Update()
    {
        if (roundIsActive)
        {
            time -= Time.deltaTime;
            timerText.text = time.ToString("#.00");

            if (time <= 0)
            {
                roundIsActive = false;
                round++;
                time = 0;
            }
        }
    }
}
