using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFlyCutscene : MonoBehaviour
{
    public float acceleration = 5.0f;
    public GameObject trail;

    private float speed = 0.0f;
    private bool isCutsceneActive = false;
    public void Begin() {
        isCutsceneActive = true;
        trail.SetActive(true);
    }

    void Update() {
        if(!isCutsceneActive) return;
        speed += acceleration * Time.deltaTime;
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
