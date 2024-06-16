using UnityEngine;
using System.Collections;

public class HeldItemSway : MonoBehaviour
{
    public float velocityMultiplier = 0.0005f;

    public Vector3 maxSway;
    public float swayScale;
    public float restSpeed;
    public float recoilRestSpeed;

    [HideInInspector]
    public float recoil;

    private float generalMultiplier = 1f;
    //private float backwardsMovement = 0f;
    public Rigidbody playerRigidbody;

    void Update() {
        if(Time.timeScale == 0) return;

        Vector3 localPos = transform.localPosition;
        Vector3 velocity = Camera.main.transform.InverseTransformDirection(playerRigidbody.velocity);

        localPos.x -= velocity.x * velocityMultiplier + Input.GetAxis("Mouse X") * swayScale * generalMultiplier;
        localPos.y -= velocity.y * 5f * velocityMultiplier + Input.GetAxis("Mouse Y") * swayScale * generalMultiplier;

        localPos.x = Mathf.Clamp(Mathf.Lerp(localPos.x, 0, restSpeed), -maxSway.x * generalMultiplier, maxSway.x * generalMultiplier);
        localPos.y = Mathf.Clamp(Mathf.Lerp(localPos.y, 0, restSpeed), -maxSway.y * generalMultiplier, maxSway.y * generalMultiplier);
        localPos.z = Mathf.Clamp(Mathf.Lerp(localPos.z, recoil, restSpeed), -maxSway.z * generalMultiplier, maxSway.z * generalMultiplier);
        localPos.z -= velocity.z * velocityMultiplier;

        recoil = Mathf.Lerp(recoil, 0, recoilRestSpeed);

        transform.localPosition = localPos;
    }

    public void AddRecoil(float amount) {
        recoil -= amount * generalMultiplier;
    }
}
