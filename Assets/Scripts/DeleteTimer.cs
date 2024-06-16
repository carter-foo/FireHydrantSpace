using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTimer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeleteMe());
    }

    IEnumerator DeleteMe() {
        yield return new WaitForSeconds(30.0f);
        Destroy(gameObject);
    }

}
