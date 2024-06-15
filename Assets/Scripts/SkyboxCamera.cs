using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SkyboxCamera : MonoBehaviour
{
    public Camera skyboxCamera;

    private void Update()
    {
        if (Camera.main == null || skyboxCamera == null) return;

        skyboxCamera.transform.rotation = Camera.main.transform.rotation;
        skyboxCamera.fieldOfView = Camera.main.fieldOfView;
    }
}
