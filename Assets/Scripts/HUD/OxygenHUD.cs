using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenHUD : MonoBehaviour
{
    public Image fullImage;
    public Image threeQuarterImage;
    public Image halfImage;
    public Image quarterImage;
    public Image emptyImage;

    public OxygenLevel oxygenLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float currentOxygen = oxygenLevel.GetCurrentOxygen();
        float maxOxygen = oxygenLevel.maxOxygen;

        if (currentOxygen >= maxOxygen * 0.75f)
        {
            SetFull();
        }
        else if (currentOxygen >= maxOxygen * 0.5f)
        {
            SetThreeQuarters();
        }
        else if (currentOxygen >= maxOxygen * 0.25f)
        {
            SetHalf();
        }
        else if (currentOxygen > maxOxygen * 0f)
        {
            SetQuarter();
        }
        else if (currentOxygen == 0f)
        {
            SetEmpty();
        }
    }

    void SetFull()
    {
        fullImage.gameObject.SetActive(true);
        threeQuarterImage.gameObject.SetActive(false);
        halfImage.gameObject.SetActive(false);
        quarterImage.gameObject.SetActive(false);
        emptyImage.gameObject.SetActive(false);
    }

    void SetThreeQuarters()
    {
        fullImage.gameObject.SetActive(false);
        threeQuarterImage.gameObject.SetActive(true);
        halfImage.gameObject.SetActive(false);
        quarterImage.gameObject.SetActive(false);
        emptyImage.gameObject.SetActive(false);
    }

    void SetHalf()
    {
        fullImage.gameObject.SetActive(false);
        threeQuarterImage.gameObject.SetActive(false);
        halfImage.gameObject.SetActive(true);
        quarterImage.gameObject.SetActive(false);
        emptyImage.gameObject.SetActive(false);
    }

    void SetQuarter()
    {
        fullImage.gameObject.SetActive(false);
        threeQuarterImage.gameObject.SetActive(false);
        halfImage.gameObject.SetActive(false);
        quarterImage.gameObject.SetActive(true);
        emptyImage.gameObject.SetActive(false);
    }

    void SetEmpty()
    {
        fullImage.gameObject.SetActive(false);
        threeQuarterImage.gameObject.SetActive(false);
        halfImage.gameObject.SetActive(false);
        quarterImage.gameObject.SetActive(false);
        emptyImage.gameObject.SetActive(true);
    }
}
