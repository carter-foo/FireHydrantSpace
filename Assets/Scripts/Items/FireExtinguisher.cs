using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    public GameObject fireExtinguisher;
    public Transform cameraTransform;
    public Vector3 itemPosition = new Vector3(0.5f, -0.5f, 1f);
    public Vector3 itemRotation = new Vector3(0f, 90f, 0f);

    private GameObject equippedItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Equip()
    {
        if (fireExtinguisher != null && cameraTransform != null)
        {
            equippedItem = Instantiate(fireExtinguisher, cameraTransform);
            equippedItem.transform.localPosition = itemPosition;
            equippedItem.transform.localEulerAngles = itemRotation;
            equippedItem.transform.localScale = Vector3.one;
        }
    }
}
