using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIcon : MonoBehaviour
{
    public Texture texture;
    public float rangeMult = 1;
    public float hardLimit = 9999;
    public bool show = true;


    public void OnDisable() {
        FindObjectOfType<HUDMarkers>()?.RemoveTarget(this);
    }
}
