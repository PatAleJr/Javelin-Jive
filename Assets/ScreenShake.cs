using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake instance;

    private float shakeMagnitude;
    private float shakeDuration;

    private float endShakeTime;

    private Transform cam;
    private Vector3 initialCamPosition;

    private void Start()
    {
        if (instance != this)
            Destroy(instance);
        instance = this;

        cam = Camera.main.transform;
        initialCamPosition = cam.transform.position;
    }

    public void Update()
    {
        if (Time.time < endShakeTime)
        {
            float offX = Random.Range(-shakeMagnitude, shakeMagnitude);
            float offY = Random.Range(-shakeMagnitude, shakeMagnitude);
            Vector3 offset = new Vector3(offX, offY, 0);
            cam.position = initialCamPosition + offset;
        }
        else {
            cam.position = initialCamPosition;
        }

    }
    public void shake(float magnitude, float duration) { 
        shakeMagnitude = magnitude;
        shakeDuration = duration;

        endShakeTime = Time.time + shakeDuration;
    }
}
