using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingShipIcon : MonoBehaviour
{
    public float flashRate;
    private TargetIcon targetIcon;
    private float t;

    void Start() {
        targetIcon = GetComponent<TargetIcon>();
    }

    void Update() {
        t += Time.deltaTime;
        if(t >= flashRate) {
            t = 0.0f;
            targetIcon.show = !targetIcon.show;
        }
    }
}
