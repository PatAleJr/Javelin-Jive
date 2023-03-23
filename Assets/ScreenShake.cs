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

    private Vector3 destination;
    private float departTime;
    private float arriveTime;

    private void Start()
    {
        if (instance != this)
            Destroy(instance);
        instance = this;

        cam = Camera.main.transform;
        initialCamPosition = cam.transform.localPosition;
    }

    public void Update()
    {
        if (Time.time < endShakeTime)
        {
            float offX = Random.Range(-shakeMagnitude, shakeMagnitude);
            float offY = Random.Range(-shakeMagnitude, shakeMagnitude);
            Vector3 offset = new Vector3(offX, offY, 0);
            cam.localPosition = initialCamPosition + offset;
        }
        else {
            cam.localPosition = initialCamPosition;
            shakeMagnitude = 0;
        }

        if (destination == Vector3.zero) return;
        transform.position = Vector3.Lerp(transform.position, destination, (Time.time - departTime) / (arriveTime - departTime));
        if ((transform.position - destination).magnitude < 0.01f) {
            destination = Vector3.zero;
            GameManager.instance.BeginRound();
        }
    }
    public void shake(float magnitude, float duration) {
        if (magnitude < shakeMagnitude) return;
        shakeMagnitude = magnitude;
        shakeDuration = duration;

        endShakeTime = Time.time + shakeDuration;
    }

    public void moveToDestination(Vector3 destination, float travelTime) {
        this.destination = destination;
        departTime = Time.time;
        arriveTime = Time.time + travelTime;
    }
}
