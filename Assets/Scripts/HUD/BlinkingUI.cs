using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingUI : MonoBehaviour
{
    public Image oxygenImage;
    public float blinkSpeed = 1.0f;
    private bool isBlinking = false;

    // Start is called before the first frame update
    void Start()
    {
        if (oxygenImage == null)
        {
            oxygenImage = GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isBlinking)
        {
            float alpha = Mathf.PingPong(Time.time * blinkSpeed, 1f);
            SetAlpha(alpha);
        }
    }

    public void StartBlinking()
    {
        isBlinking = true;
    }

    public void StopBlinking()
    {
        isBlinking = false;
        SetAlpha(1f); // Reset alpha to fully visible
    }

    private void SetAlpha(float alpha)
    {
        if (oxygenImage != null)
        {
            Color color = oxygenImage.color;
            color.a = alpha;
            oxygenImage.color = color;
        }
    }
}
