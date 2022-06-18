using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObj : MonoBehaviour
{
    public float fTime;

    private void OnEnable()
    {
        CancelInvoke();
        Invoke("Disable", fTime);
    }


    void Disable()
    {
        gameObject.SetActive(false);
    }
}
