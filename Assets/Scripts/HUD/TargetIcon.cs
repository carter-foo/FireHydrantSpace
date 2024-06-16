using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIcon : MonoBehaviour
{
    public float rangeMult = 1;
    public float hardLimit = 9999;
    public bool show = true;


    public void OnDisable() {
        GameObject.FindObjectOfType<HUDMarkers>().RemoveTarget(this);
    }
}
