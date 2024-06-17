using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingShipIcon : MonoBehaviour
{
    public float growthRate = 0.02f;
    private TargetIcon targetIcon;

    void Start() {
        targetIcon = GetComponent<TargetIcon>();
    }

    void Update() {
        targetIcon.rangeMult += growthRate * Time.deltaTime;
    }
}
